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
        private AppointmentContext dbAppointments = new AppointmentContext();
        private NewsContext dbNews = new NewsContext();

        // GET: Vaccine
        public ActionResult Index()
        {
            return View(dbAppointments.Vaccines.ToList());
        }

        // GET: Vaccine/Edit
        public ActionResult Edit(string param) {
            var query = from a in dbAppointments.Vaccines
                        where a.Name.Contains(param)
                        select a;

            VaccineModel v = (from vaccine in dbAppointments.Vaccines where vaccine.Name == param select vaccine).FirstOrDefault();
            VaccineEditViewModel model = new VaccineEditViewModel();
            model.Vaccine = v;
            model.NewDoses = 0;

            return View(model);
        }

        // POST: Vaccine/Edit
        [HttpPost]
        public ActionResult Edit(VaccineEditViewModel model) {
            if (ModelState.IsValid) {
                // Change values in Vaccine Tables
                var v = dbAppointments.Vaccines.FirstOrDefault(x => x.Name == model.Vaccine.Name);
                v.NumOfDosesAvailable = model.Vaccine.NumOfDosesAvailable + model.NewDoses;
                dbAppointments.SaveChanges();

                // Add news
                ChangeInVaccinesNewsModel c = new ChangeInVaccinesNewsModel();
                c.Date = DateTime.Now;
                switch(model.Vaccine.Name)
                {
                    case "AstraZeneca":
                        c.AstraZenecaChange = model.NewDoses;
                        break;
                    case "Pfizer":
                        c.PfizerChange = model.NewDoses;
                        break;
                    case "Sputnik":
                        c.SputnikChange = model.NewDoses;
                        break;
                    case "Sinopharm":
                        c.SinopharmChange = model.NewDoses;
                        break;
                    default:
                        break;
                }
                dbNews.ChangeInVaccinesNews.Add(c);
                dbNews.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}
