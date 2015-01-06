using Aphrodite.Front.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;


namespace Aphrodite.Front.Controllers
{
    public class MatchController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Match
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            var Model = db.Matches.Where(x => x.SenderId == userId);
            return View(Model);
        }
    }
}