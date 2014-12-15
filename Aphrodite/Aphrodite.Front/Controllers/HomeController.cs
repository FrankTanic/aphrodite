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
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public int ReceivedApprove;
        public int SentApprove;


        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {

                // Get your preferences
                string userId = User.Identity.GetUserId();
                var prefQuery = db.Users.Where(x => x.Id == userId).Single();

                ProfileViewModel userPrefProfile = new ProfileViewModel
                {
                    ID = prefQuery.Id,
                    SexualPreference = prefQuery.SexualPreference,
                };

                var profileQuery = (from u in db.Users.Where(x => x.Id != userId && x.SexualPreference != userPrefProfile.SexualPreference)
                                    join m in db.Matches
                                        on u.Id equals m.ReceiverId into um
     
                                    where !um.Any(x => x.SenderId == userId)
                                    select u).FirstOrDefault();


                if (profileQuery != null)
                {
                    ProfileViewModel userProfile = new ProfileViewModel
                    {
                        ID = profileQuery.Id,
                        DisplayName = profileQuery.DisplayName,

                    };


                    var photoQuery = (from p in db.Photo
                                      where p.UserID == profileQuery.Id
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
                else
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
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
       
  

    }
}