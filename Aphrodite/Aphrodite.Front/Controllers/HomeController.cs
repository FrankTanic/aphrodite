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
using Aphrodite.Front.Hubs;
using Microsoft.AspNet.SignalR;

namespace Aphrodite.Front.Controllers
{
    [System.Web.Mvc.AuthorizeAttribute]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var userProfile = GetUserProfile();

                if (userProfile == null)
                {
                    return View();
                }
            
            return View(userProfile);
        }

        public ProfileViewModel GetUserProfile()
        {
            // Get your preferences
            string userId = User.Identity.GetUserId();
            var user = db.Users.Where(x => x.Id == userId).Single();

            var gender = GetPartnerGender(user);
            var preference = GetPartnerSexualPreference(user);
            var partner = FindPartner(gender, preference, userId);

            if (partner == null)
            {
                return null;
            }

            ProfileViewModel userProfile = new ProfileViewModel
            {
                ID = partner.Id,
                DisplayName = partner.DisplayName,
                Years = GetAge(partner.BirthDay)
            };

            var photoQuery = (from p in db.Photos
                              where p.UserID == partner.Id
                              select p).SingleOrDefault();

            if (photoQuery == null)
            {
                ViewBag.PhotoFile = "~/Content/img/no-image.png";
            }
            else
            {
                ViewBag.PhotoFile = "~/Content/Upload/" + photoQuery.File;
            }

            return userProfile;
        }

        public async Task<ActionResult> Like(string id)
        {
            string userId = User.Identity.GetUserId();
            int like = 1;

            MatchViewModel match = new MatchViewModel
            {
                SenderId = userId,
                ReceiverId = id,
                Approve = like
            };

            db.Entry(match).State = EntityState.Added;
            await db.SaveChangesAsync();

            UpdateCount(id);

            //return RedirectToAction("Index");
            return View("ProfilePartialAjax",GetUserProfile());
        }

        public async Task<ActionResult> Dislike(string id)
        {
            string userId = User.Identity.GetUserId();
            int like = 0;

            MatchViewModel match = new MatchViewModel
            {
                SenderId = userId,
                ReceiverId = id,
                Approve = like
            };

            db.Entry(match).State = EntityState.Added;
            await db.SaveChangesAsync();

            UpdateCount(id);

            return View("ProfilePartialAjax", GetUserProfile());
        }

        public int GetAge(DateTime birthday)
        {
            int age = DateTime.Now.Year - birthday.Year;

            if (DateTime.Now.Month < birthday.Month || (DateTime.Now.Month == birthday.Month && DateTime.Now.Day < birthday.Day)) age--;
            return age;
        }

        private Gender GetPartnerGender(ApplicationUser user)
        {
            return user.SexualPreference == SexualPreference.Male ? Gender.Male : Gender.Female;
        }

        private SexualPreference GetPartnerSexualPreference(ApplicationUser user)
        {
            return user.Gender == Gender.Male ? SexualPreference.Male : SexualPreference.Female;
        }

        private ApplicationUser FindPartner(Gender gender, SexualPreference preference, string userId)
        {
            return (from u in db.Users.Where(x => x.Id != userId && x.Gender == gender && x.SexualPreference == preference)
                    join m in db.Matches
                        on u.Id equals m.ReceiverId into um

                    where !um.Any(x => x.SenderId == userId)
                    select u).OrderBy(x => Guid.NewGuid()).FirstOrDefault();
        }

        public void UpdateCount(string id)
        {
            string userId = User.Identity.GetUserId();
            int count = GetMatchCount();
            int countOpposite = GetMatchCountOpposite(id);

            IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<MatchesHub>();

            hubContext.Clients.Group(userId).addCount(count);
            hubContext.Clients.Group(id).addCount(countOpposite);

            if (count == 1)
            {
                hubContext.Clients.Group(userId).addNotification("Je hebt een nieuwe match!");
            }
            else
            {
                if (count > 1)
                {
                    hubContext.Clients.Group(userId).addNotification("Je hebt nieuwe matches!");
                }
            }

            if (countOpposite == 1)
            {
                hubContext.Clients.Group(id).addNotification("Je hebt een nieuwe match!");
            }
            else
            {
                if (countOpposite > 1)
                {
                    hubContext.Clients.Group(id).addNotification("Je hebt nieuwe matches!");
                }
            }
        }

        public int GetMatchCount()
        {
            string userId =  User.Identity.GetUserId();

            int matchCount = (from mine in db.Matches
                              from theirs in db.Matches
                              from name in db.Users
                              where mine.SenderId == userId && theirs.ReceiverId == userId && mine.ReceiverId == theirs.SenderId && mine.Approve == 1 && theirs.Approve == 1 && name.Id == theirs.SenderId
                              select new matches
                              {
                                  Id = theirs.SenderId,
                              }).Count();

            return (matchCount);
        }

        public int GetMatchCountOpposite(string id)
        {
            string userId = id;

            int matchCount = (from mine in db.Matches
                              from theirs in db.Matches
                              from name in db.Users
                              where mine.SenderId == userId && theirs.ReceiverId == userId && mine.ReceiverId == theirs.SenderId && mine.Approve == 1 && theirs.Approve == 1 && name.Id == theirs.SenderId
                              select new matches
                              {
                                  Id = theirs.SenderId,
                              }).Count();

            return (matchCount);
        }

    }
}