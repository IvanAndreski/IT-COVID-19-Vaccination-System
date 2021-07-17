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
    [Authorize(Roles = "Administrator")]
    public class VaccineController : Controller
    {
        private AppointmentContext db = new AppointmentContext();

        // GET: Vaccine/Edit
        public ActionResult Edit(string param) {
            var query = from a in db.Vaccines
                        where a.Name.Contains(param)
                        select a;

            VaccineModel model = (from vaccine in db.Vaccines where vaccine.Name == param select vaccine).FirstOrDefault();
            return View(model);
        }

        // POST: Vaccine/Edit
        [HttpPost]
        public ActionResult Edit(VaccineModel model) {
            if (ModelState.IsValid) {
                var v = db.Vaccines.FirstOrDefault(x => x.Name == model.Name);
                v.NumOfDosesAvailable = model.NumOfDosesAvailable;
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        // GET: Vaccine
        public ActionResult Index()
        {
            return View(db.Vaccines.ToList());
        }

        // GET: Vaccine/Details/5
        public ActionResult Details(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VaccinationsAtDay vaccinationsAtDay = db.VaccinationsAtDay.Find(id);
            if (vaccinationsAtDay == null)
            {
                return HttpNotFound();
            }
            return View(vaccinationsAtDay);
        }

        // GET: Vaccine/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vaccine/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Day,NumVaccinationsAtDay")] VaccinationsAtDay vaccinationsAtDay)
        {
            if (ModelState.IsValid)
            {
                db.VaccinationsAtDay.Add(vaccinationsAtDay);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vaccinationsAtDay);
        }

        // GET: Vaccine/Delete/5
        public ActionResult Delete(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VaccinationsAtDay vaccinationsAtDay = db.VaccinationsAtDay.Find(id);
            if (vaccinationsAtDay == null)
            {
                return HttpNotFound();
            }
            return View(vaccinationsAtDay);
        }

        // POST: Vaccine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(DateTime id)
        {
            VaccinationsAtDay vaccinationsAtDay = db.VaccinationsAtDay.Find(id);
            db.VaccinationsAtDay.Remove(vaccinationsAtDay);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
