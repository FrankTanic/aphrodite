using Aphrodite.Front.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Aphrodite.Front.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var UID = User.Identity.GetUserId();
                var userProfile = db.Users.Where(x => x.Id == UID);

                List<ProfileViewModel> profiles = new List<ProfileViewModel>();


                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

    }
}