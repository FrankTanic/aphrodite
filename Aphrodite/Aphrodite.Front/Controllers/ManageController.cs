using Aphrodite.Front.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Globalization;
using System.Drawing;

namespace Aphrodite.Front.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Manage/Index
        public ActionResult Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage = TempData["UploadMessage"] ?? GetErrorMessage(message);

            string userID = User.Identity.GetUserId();
            var user = db.Users.Find(userID);
            var photo = db.Photos.Where(x => x.UserID == userID).FirstOrDefault();
            DateTime birthday = user.BirthDay;

            var profile = new ProfileViewModel()
            {
                DisplayName = user.DisplayName,
                Email = UserManager.GetEmail(userID),
                Birthday = birthday,
                Gender = user.Gender,
                SexualPreference = user.SexualPreference,
                Photo = (photo == null) ? "~/Content/img/no-image.png" : "~/Content/Upload/" + photo.File
            };

            return View(profile);
        }

        //
        // GET: /Manage/ChangeData
        public ActionResult ChangeData()
        {
            string Id = User.Identity.GetUserId();
            var user = db.Users.Where(x => x.Id == Id).Single();
            DateTime dateAndTime = user.BirthDay;
            int year = dateAndTime.Year;
            int month = dateAndTime.Month;
            int day = dateAndTime.Day;

            EditViewModel edit = new EditViewModel
            {
                BirthDayDay = day,
                BirthDayMonth = month,
                BirthDayYear = year,
                SexualPreference = user.SexualPreference,
                DisplayName = user.DisplayName,
                Email = user.Email
            };

            ViewBag.id = Id;
            return View(edit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeData(EditViewModel model)
        {
            string UID = User.Identity.GetUserId();
            var user = db.Users.Where(x => x.Id == UID).Single();
            DateTime Birthday = new DateTime(model.BirthDayYear, model.BirthDayMonth, model.BirthDayDay);

            if (ModelState.IsValid)
            {
                if(user.Email != model.Email)
                {
                    var email = db.Users.Where(x => x.Email == model.Email).FirstOrDefault();

                    if(email != null)
                    {
                        ModelState.AddModelError("Email", "Dit e-mailadres is al ingebruik");
                        return View(model);
                    }
                    else
                    {
                        user.Email = model.Email;
                        user.UserName = model.Email;
                    }
                }

                user.BirthDay = Birthday;
                user.SexualPreference = model.SexualPreference;
                user.Gender = model.Gender;
                user.DisplayName = model.DisplayName;

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Manage", new { Message = ManageMessageId.ChangeProfileData });
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength == 0)
            {
                TempData["UploadMessage"] = "Kies een foto van jezelf";
                return RedirectToAction("Index");
            }

            string extension = Path.GetExtension(file.FileName);
            string[] acceptedExtensions = new string[] { ".jpg", ".png", ".jpeg", ".jpe", ".gif", ".bmp" };

            if (!acceptedExtensions.Contains(extension))
            {
                TempData["UploadMessage"] = "Dit bestandsformaat word niet geaccepteerd";
                return RedirectToAction("Index");
            }


            string fileId = Guid.NewGuid().ToString();
            string fileName = String.Format("{0}.{1}", fileId, extension);
            string path = Path.Combine(Server.MapPath("~/Content/Upload"), fileName);                            
            file.SaveAs(path);

            UserPhoto photo = new UserPhoto
            {
                UserID = User.Identity.GetUserId(),
                File = fileName
            };          
            db.Photos.Add(photo);
            db.SaveChanges();

            TempData["UploadMessage"] = "Upload succesvol";
     
            return RedirectToAction("Index");
        }

        //
        // GET: /Manage/RemoveLogin
        public ActionResult RemoveLogin()
        {
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return View(linkedAccounts);
        }

        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInAsync(user, isPersistent: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInAsync(user, isPersistent: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInAsync(user, isPersistent: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage = GetErrorMessage(message);

            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

#region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        private string GetErrorMessage(ManageMessageId? message)
        {
            if (message == null)
            {
                return String.Empty;
            }

            switch (message)
            {
                case ManageMessageId.ChangeProfileData:
                    return "Je gegevens zijn aangepast";

                case ManageMessageId.AddPhoneSuccess:
                    return "Met succes een telefoon toegevoegd. Hoera.";

                case ManageMessageId.ChangePasswordSuccess:
                    return "Je wachtwoord is gewijzigd.";

                case ManageMessageId.Error:
                    return "Foutje, hoor.";

                case ManageMessageId.RemoveLoginSuccess:
                    return "Het succesvolle inloggen is verwijderd.";

                case ManageMessageId.RemovePhoneSuccess:
                    return "Het telefoonsucces is verwijderd.";

                case ManageMessageId.SetPasswordSuccess:
                    return "Wachtwoord is gezet.";

                case ManageMessageId.SetTwoFactorSuccess:
                    return "Twee factoren zijn goed gegaan.";

                default:
                    return "Ik geef het op.";
            }
        }

        public enum ManageMessageId
        {
            ChangeProfileData,
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

#endregion
    }
}