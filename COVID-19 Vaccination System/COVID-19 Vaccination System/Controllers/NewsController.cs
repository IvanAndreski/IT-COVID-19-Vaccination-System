using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using COVID_19_Vaccination_System.Models;

namespace COVID_19_Vaccination_System.Controllers
{
    public class NewsController : Controller
    {
        private NewsContext dbNews = new NewsContext();
        private AppointmentContext dbAppointment = new AppointmentContext();

        // GET: News
        public ActionResult Index()
        {
            NewsViewModel model = new NewsViewModel();
            model.ChangeInVaccinesNewsList = dbNews.ChangeInVaccinesNews.ToList();
            model.VaccinationsNewsList = dbNews.VaccinationsNews.ToList();
            model.AvailableVaccineList = dbAppointment.Vaccines.ToList();

            model.ChangeInVaccinesNewsList.Reverse();
            model.VaccinationsNewsList.Reverse();

            return View(model);
        }
    }
}
