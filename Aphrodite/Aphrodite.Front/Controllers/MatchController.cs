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
            var matches = GetMatches();
            int count = matches.Count;
            return View(matches);
        }
        public List<matches> GetMatches()
        {
            string userId = User.Identity.GetUserId();
            List<matches> matches = (from mine in db.Matches
            from theirs in db.Matches
            from name in db.Users
            where mine.SenderId == userId && theirs.ReceiverId == userId && mine.ReceiverId == theirs.SenderId && mine.Approve == 1 && theirs.Approve == 1 && name.Id == theirs.SenderId
            select new matches
            {
                Id = theirs.SenderId,
                Name = name.DisplayName,
               // count = mine.ReceiverId.Count(),
            }).ToList();
            return (matches);
        }

    }
}