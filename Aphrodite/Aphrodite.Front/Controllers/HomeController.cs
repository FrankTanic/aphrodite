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

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {

                // Get your preferences
                string userId = User.Identity.GetUserId();
                var prefQuery = db.Users.Where(x => x.Id == userId).Single();

                DateTime birthday = prefQuery.BirthDay;

                var years = BirthdayYears(birthday, DateTime.Now);

                ProfileViewModel userPrefProfile = new ProfileViewModel
                {
                    ID = prefQuery.Id,
                    SexualPreference = prefQuery.SexualPreference,
                };

                if(prefQuery.Gender == Gender.Man)
                {
                    if(prefQuery.SexualPreference == SexualPreference.Man)
                    {
                        prefQuery.SexualPreference = SexualPreference.Man;
                    }
                    else
                    {
                        prefQuery.Gender = Gender.Vrouw;
                        prefQuery.SexualPreference = SexualPreference.Man;
                    }
                }
                else
                {
                    if(prefQuery.SexualPreference == SexualPreference.Vrouw)
                    {
                        prefQuery.SexualPreference = SexualPreference.Vrouw;
                    }
                    else
                    {
                        prefQuery.Gender = Gender.Man;
                        prefQuery.SexualPreference = SexualPreference.Vrouw;
                    }
                }

                    var profileQuery = (from u in db.Users.Where(x => x.Id != userId && x.Gender == prefQuery.Gender && x.SexualPreference == prefQuery.SexualPreference)
                                        join m in db.Matches
                                            on u.Id equals m.ReceiverId into um

                                        where !um.Any(x => x.SenderId == userId)
                                        select u).OrderBy(x => Guid.NewGuid()).FirstOrDefault();

                if (profileQuery != null)
                {
                    ProfileViewModel userProfile = new ProfileViewModel
                    {
                        ID = profileQuery.Id,
                        DisplayName = profileQuery.DisplayName,
                        Years = years
                    };

<<<<<<< HEAD

=======
>>>>>>> c9e8b43c6f8ee24ab1027bf0e0ab5e41af46a3ae
                    var photoQuery = (from p in db.Photos
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

        public int BirthdayYears(DateTime birthday, DateTime now)
        {
            int age = now.Year - birthday.Year;

            if (now.Month < birthday.Month || (now.Month == birthday.Month && now.Day < birthday.Day)) age--;
            return age;
        }
    }
}