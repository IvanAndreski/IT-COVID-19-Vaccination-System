using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using COVID_19_Vaccination_System.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace COVID_19_Vaccination_System.Controllers
{
    public class HomeController : Controller
    {
        AccountController accountController = new AccountController();

        public ActionResult Index()
        {
            Console.WriteLine("Yo");
            if (User.Identity.IsAuthenticated)
            {
                ApplicationUser user = System.Web.HttpContext
                    .Current
                    .GetOwinContext()
                    .GetUserManager<ApplicationUserManager>()
                    .FindByEmail(User.Identity.Name);

                ViewBag.FullName = String.Format("{0} {1}",
                    user.FirstName,
                    user.LastName
                );
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}