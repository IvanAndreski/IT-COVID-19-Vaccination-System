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

namespace COVID_19_Vaccination_System.Controllers {
    [Authorize]
    public class AppointmentController : Controller {
        private AppointmentContext db = new AppointmentContext();

        // Constants
        private static readonly int MAX_VACCINATIONS_PER_DAY = 50;
        private static DateTime FIRST_DAY_OF_VACCINATION = DateTime.Parse("Sep 1, 2021");

        // GET: Appointment
        public ActionResult Index() {
            return View(db.Appointments.Where(x => x.EmailOfUser == User.Identity.Name).ToList());
        }

        // GET: Appointment/Create
        public ActionResult Create() {
            // Check if user already hass made an appointment
            var a = db.Appointments.Where(x => x.EmailOfUser == User.Identity.Name).ToList();
            if (a.Count() > 0)
            {
                return AppointmentExistsError(a);
            }

            ViewBag.ListOfVaccines = db.Vaccines.ToList();

            return View();
        }

        // POST: Appointment/NewAppointment
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EmailOfUser,Date,NameOfVaccine")] AppointmentModel model) {
            model.EmailOfUser = User.Identity.Name;

            // Find first day in the database that has vaccination appointmennts less than MAX_VACCINATIONS_PER_DAY
            var day = db.VaccinationsAtDay.FirstOrDefault(x => x.NumVaccinationsAtDay < MAX_VACCINATIONS_PER_DAY);
            model.Date = day.Day.AddMinutes(day.NumVaccinationsAtDay * 15);
            model.AppointmentNum = 1;
            model.Confirmed = false;
            db.Appointments.Add(model);

            // Edit the number of available doses
            var v = db.Vaccines.FirstOrDefault(x => x.Name == model.NameOfVaccine);
            v.NumOfDosesAvailable--;

            // Change the number of vaccinations at that day
            day.NumVaccinationsAtDay++;

            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        // GET: Appointment/AppointmentExistsError
        public ActionResult AppointmentExistsError(IEnumerable<AppointmentModel> model)
        {
            return View("AppointmentExistsError", model);
        }

        // GET: Appointment/Delete/
        public ActionResult Delete(int aNum) {
            AppointmentModel model = db.Appointments.FirstOrDefault(x => x.AppointmentNum == aNum && x.EmailOfUser == User.Identity.Name);

            return Delete(model);
        }

        // POST: Appointment/Delete/
        [HttpPost]
        public ActionResult Delete(AppointmentModel model) {
            // Remove appontment
            db.Appointments.Remove(model);

            // Add to available vaccine doses
            var v = db.Vaccines.FirstOrDefault(x => model.NameOfVaccine == x.Name);
            v.NumOfDosesAvailable++;

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Appointment/AppointmentsToday
        [Authorize(Roles = "Doctor")]
        public ActionResult AppointmentsToday()
        {
            DateTime temp = DateTime.Parse("Sep 1, 2021");
            List<AppointmentModel> appointments = db.Appointments.Where(x => DbFunctions.TruncateTime(x.Date) == temp.Date).ToList();

            return View(appointments);
        }

        // GET: Appointment/Confirm
        public ActionResult Confirm(int aNum, string email)
        {
            AppointmentModel model = db.Appointments.FirstOrDefault(x => x.AppointmentNum == aNum && x.EmailOfUser == email);

            return Confirm(model);
        }

        // POST: Appointment/Confirm
        [HttpPost]
        public ActionResult Confirm(AppointmentModel model)
        {
            // Remove appontment
            model.Confirmed = true;

            // Find vaccinne in database
            var v = db.Vaccines.FirstOrDefault(x => x.Name == model.NameOfVaccine);

            // Create second appointment
            AppointmentModel secondAppointment = new AppointmentModel();
            secondAppointment.AppointmentNum = 2;
            secondAppointment.Confirmed = false;
            secondAppointment.EmailOfUser = model.EmailOfUser;
            secondAppointment.NameOfVaccine = model.NameOfVaccine;
            secondAppointment.Date = model.Date.AddDays(v.TimeBetweenDoses);

            // Add second appointment
            db.Appointments.Add(secondAppointment);

            db.SaveChanges();

            return RedirectToAction("AppointmentsToday");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
