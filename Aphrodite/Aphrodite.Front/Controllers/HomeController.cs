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
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            // Get your preferences
            string userId = User.Identity.GetUserId();
            var user = db.Users.Where(x => x.Id == userId).Single();

            var gender = GetPartnerGender(user);
            var preference = GetPartnerSexualPreference(user);
            var partner = FindPartner(gender, preference, userId);

            if (partner == null)
            {
                return View();
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

            return View(userProfile);
        }


        public ActionResult Like(string id)
        {
            var userId = User.Identity.GetUserId();

            int like = 1;

            MatchViewModel match = new MatchViewModel
            {
                SenderId = userId,
                ReceiverId = id,
                Approve = like
            };

            db.Entry(match).State = EntityState.Added;
            db.SaveChanges();

            return RedirectToAction("Index");
      
        }

        public ActionResult Dislike(string id)
        {
            var userId = User.Identity.GetUserId();

            int like = 0;

            MatchViewModel match = new MatchViewModel
            {
                SenderId = userId,
                ReceiverId = id,
                Approve = like
            };

            db.Entry(match).State = EntityState.Added;
            db.SaveChanges();


            return RedirectToAction("Index");

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
    }
}