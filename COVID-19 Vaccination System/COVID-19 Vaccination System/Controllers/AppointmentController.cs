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
        private static readonly int NUM_OF_VACCINATIONS_PER_DAY = 50;
        private static DateTime FIRST_DAY_OF_VACCINATION = DateTime.Parse("Sep 1, 2021");

        // GET: Appointment
        public ActionResult Index() {
            return View(db.Appointments.ToList());
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

        // GET: Appointment/NewAppointment
        public ActionResult NewAppointment() {
            ViewBag.ListOfVaccines = db.Vaccines.ToList();

            return View();
        }

        // POST: Appointment/NewAppointment
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewAppointment([Bind(Include = "Id,EmailOfUser,Date,NameOfVaccine")] AppointmentModel appointmentModel) {
            appointmentModel.EmailOfUser = User.Identity.Name;


            //db.Appointments.Add(appointmentModel);
            //db.SaveChanges();
            //return RedirectToAction("Index");


            return RedirectToAction("Index", "Home");
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
