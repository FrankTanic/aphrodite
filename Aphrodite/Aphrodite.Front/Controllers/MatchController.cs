using Aphrodite.Front.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aphrodite.Front.Controllers
{
    public class MatchController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Match
        public ActionResult Index()
        {

            return View();
        }
    }
}