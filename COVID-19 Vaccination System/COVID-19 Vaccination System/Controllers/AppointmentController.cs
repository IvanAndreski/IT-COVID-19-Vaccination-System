using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using COVID_19_Vaccination_System.Models;

namespace COVID_19_Vaccination_System.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private AppointmentContext dbAppointments = new AppointmentContext();
        private NewsContext dbNews = new NewsContext();

        // Constants
        private static int MAX_VACCINATIONS_PER_DAY = 50;
        private static DateTime FIRST_DAY_OF_VACCINATION = DateTime.Parse("Sep 1, 2021");

        // GET: Appointment
        public ActionResult Index()
        {
            // If user is doctor instruct him/her to create a private account
            if (User.IsInRole("Doctor"))
            {
                return View("UserIsDoctorError");
            }

            return View(dbAppointments.Appointments.Where(x => x.EmailOfUser == User.Identity.Name).ToList());
        }

        // GET: Appointment/Create
        public ActionResult Create()
        {
            // If user is doctor instruct him/her to create a private account
            if(User.IsInRole("Doctor"))
            {
                return View("UserIsDoctorError");
            }

            // Check if user already has made an appointment
            var a = dbAppointments.Appointments
                .Where(x => x.EmailOfUser == User.Identity.Name)
                .ToList();
            if (a.Count() > 0)
            {
                if (a[0].Date.Date > DateTime.Today.Date)
                {
                    return AppointmentExistsError(a);
                }
            }

            var model = new CreateAppointmentViewModel();
            model.Appointment = new AppointmentModel();
            model.VaccineList = dbAppointments.Vaccines.ToList();
            model.AvailableVaccineNameList = model.VaccineList
                .Where(x => x.NumOfDosesAvailable > 0)
                .Select(x => x.Name)
                .ToList();
            if (model.AvailableVaccineNameList.Count > 0)
                model.CanAppoint = true;
            else
                model.CanAppoint = false;
            model.AvailableDays = dbAppointments.VaccinationsAtDay
                .Where(x => DbFunctions.TruncateTime(x.Day) >
                    DbFunctions.TruncateTime(DateTime.Today))
                .Where(x => x.NumVaccinationsAtDay < MAX_VACCINATIONS_PER_DAY)
                .Select(x => x.Day)
                .Take(90)
                .ToList();

            return View(model);
        }

        // POST: Appointment/NewAppointment
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateAppointmentViewModel viewModel)
        {
            AppointmentModel model = viewModel.Appointment;
            model.EmailOfUser = User.Identity.Name;

            // Find last appointment at that day
            List<DateTime> tempDate = dbAppointments.Appointments
                .Where(x => DbFunctions.TruncateTime(x.Date) ==
                    DbFunctions.TruncateTime(model.Date))
                .Select(x => x.Date)
                .ToList();
            DateTime d;
            if(tempDate.Count == 0)
            {
                d = model.Date;
            }
            else
            {
                d = tempDate.Max().AddMinutes(15);
            }
            model.Date = d;
            model.AppointmentNum = 1;
            model.Confirmed = false;
            dbAppointments.Appointments.Add(model);

            // Edit the number of available doses
            var v = dbAppointments.Vaccines.FirstOrDefault(x => x.Name == model.NameOfVaccine);
            v.NumOfDosesAvailable--;

            // Change the number of vaccinations at that day
            dbAppointments.VaccinationsAtDay
                .FirstOrDefault(x => DbFunctions.TruncateTime(x.Day) ==
                    DbFunctions.TruncateTime(model.Date))
                .NumVaccinationsAtDay++;

            dbAppointments.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        // GET: Appointment/AppointmentExistsError
        public ActionResult AppointmentExistsError(IEnumerable<AppointmentModel> model)
        {
            return View("AppointmentExistsError", model);
        }

        // GET: Appointment/Delete/
        [Authorize(Roles = "User,Administrator")]
        public ActionResult Delete(int aNum)
        {
            AppointmentModel model = dbAppointments.Appointments
                .Where(x => DbFunctions.TruncateTime(x.Date) >
                    DbFunctions.TruncateTime(DateTime.Today))
                .FirstOrDefault(x => x.AppointmentNum == aNum && x.EmailOfUser == User.Identity.Name);

            return Delete(model);
        }

        // POST: Appointment/Delete/
        [HttpPost]
        public ActionResult Delete(AppointmentModel model)
        {
            // Check if second appointment
            if(model.AppointmentNum == 2)
            {
                return View("DeleteSecondAppointmentError");
            }

            // Remove appontment
            dbAppointments.Appointments.Remove(model);

            // Add to available vaccine doses
            var v = dbAppointments
                .Vaccines
                .FirstOrDefault(x => model.NameOfVaccine == x.Name);
            v.NumOfDosesAvailable++;

            // Remove one slot of vaccinations per day
            var d = dbAppointments.VaccinationsAtDay
                .FirstOrDefault(x => DbFunctions.TruncateTime(x.Day) ==
                    DbFunctions.TruncateTime(model.Date));
            d.NumVaccinationsAtDay--;

            dbAppointments.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Appointment/AppointmentsToday
        [Authorize(Roles = "Doctor")]
        public ActionResult AppointmentsToday()
        {
            DateTime temp = DateTime.Today;
            List<AppointmentModel> appointments = dbAppointments
                .Appointments
                .Where(x => DbFunctions.TruncateTime(x.Date) == 
                    DbFunctions.TruncateTime(temp.Date))
                .ToList();

            return View(appointments);
        }

        // GET: Appointment/Confirm
        [Authorize(Roles = "Doctor")]
        public ActionResult Confirm(int aNum, string email)
        {
            AppointmentModel model = dbAppointments.Appointments
                .Where(x => DbFunctions.TruncateTime(x.Date) == 
                    DbFunctions.TruncateTime(DateTime.Today))
                .FirstOrDefault(x => x.AppointmentNum == aNum && x.EmailOfUser == email);

            return Confirm(model);
        }

        // POST: Appointment/Confirm
        [HttpPost]
        public ActionResult Confirm(AppointmentModel model)
        {
            // Confirm appointment
            var a = dbAppointments.Appointments
                .FirstOrDefault(x => x.Date == model.Date &&
                    x.EmailOfUser == model.EmailOfUser);
            a.Confirmed = true;

            // Find vaccinne in database
            var v = dbAppointments.Vaccines
                .FirstOrDefault(x => x.Name == model.NameOfVaccine);

            // Create second appointment if first
            if (model.AppointmentNum == 1)
            {
                AppointmentModel secondAppointment = new AppointmentModel();
                secondAppointment.AppointmentNum = 2;
                secondAppointment.Confirmed = false;
                secondAppointment.EmailOfUser = model.EmailOfUser;
                secondAppointment.NameOfVaccine = model.NameOfVaccine;
                secondAppointment.Date = model.Date.AddDays(v.TimeBetweenDoses);

                // Add second appointment
                dbAppointments.Appointments.Add(secondAppointment);

                // Edit news table
                var id = DateTime.Today;
                var news = dbNews.VaccinationsNews.FirstOrDefault(x => DbFunctions.TruncateTime(x.Day) ==
                    DbFunctions.TruncateTime(DateTime.Today));
                if (news != null)
                {
                    news.Vaccinations = news.Vaccinations + 1;
                }
                else
                {
                    news = new VaccinationsNewsModel();
                    news.Day = id;
                    news.Vaccinations = 1;
                    dbNews.VaccinationsNews.Add(news);
                }
            }

            dbAppointments.SaveChanges();
            dbNews.SaveChanges();

            return RedirectToAction("AppointmentsToday");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbAppointments.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
