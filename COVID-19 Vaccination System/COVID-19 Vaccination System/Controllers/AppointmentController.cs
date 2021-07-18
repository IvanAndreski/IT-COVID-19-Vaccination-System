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

        // GET: Appointment/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppointmentModel appointmentModel = db.Appointments.Find(id);
            if (appointmentModel == null) {
                return HttpNotFound();
            }
            return View(appointmentModel);
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

        // GET: Appointment/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppointmentModel appointmentModel = db.Appointments.Find(id);
            if (appointmentModel == null) {
                return HttpNotFound();
            }
            return View(appointmentModel);
        }

        // POST: Appointment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmailOfUser,Date,NameOfVaccine")] AppointmentModel appointmentModel) {
            if (ModelState.IsValid) {
                db.Entry(appointmentModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appointmentModel);
        }

        // GET: Appointment/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppointmentModel appointmentModel = db.Appointments.Find(id);
            if (appointmentModel == null) {
                return HttpNotFound();
            }
            return View(appointmentModel);
        }

        // POST: Appointment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            AppointmentModel appointmentModel = db.Appointments.Find(id);
            db.Appointments.Remove(appointmentModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
