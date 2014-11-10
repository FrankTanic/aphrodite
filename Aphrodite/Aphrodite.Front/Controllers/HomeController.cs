using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aphrodite.Business;

namespace Aphrodite.Front.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var client = new UserClient();
            ViewBag.Users = client.GetUsers();

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