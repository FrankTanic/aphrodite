﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aphrodite.Front.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {

        [Required(ErrorMessage = "Vul je voornaam naam in")]
        [StringLength(255, ErrorMessage = "Je voornaam naam moet uit minimaal 2 tekens en max 255 tekens bestaan", MinimumLength = 2)]
        [Display(Name = "Voornaam")]
        public string DisplayName { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public SexualPreference SexualPreference { get; set; }

        [Required(ErrorMessage = "Geef een geboorte dag op")]
        [Range(1, 31, ErrorMessage = "Dit is geen geldige dag")]
        public int BirthDayDay { get; set; }

        [Required(ErrorMessage = "Geef een geboorte maand op")]
        [Range(1, 12, ErrorMessage = "Dit is geen geldige maand")]
        public int BirthDayMonth { get; set; }

        [Required(ErrorMessage = "Geef een geboorte jaar op")]
        [Range(1901, 2014, ErrorMessage = "Dit is geen geldig jaar")]
        public int BirthDayYear { get; set; }

        [Required]
        [Display(Name = "Geboortedatum")]
        [CustomValidation(typeof(ValidationCheck), "IsEigtheenPlus")]
        public DateTime? BirthDay
        {
            get
            {
                try
                {
                    return new DateTime(BirthDayYear, BirthDayMonth, BirthDayDay);
                }
                catch
                {
                    return DateTime.MinValue;
                }
            }
        }

        [Required(ErrorMessage = "Vul je e-mailadres in")]
        [EmailAddress(ErrorMessage = "Dit is geen geldig e-mailadres")]
        [Display(Name = "Email")]
        [CustomValidation(typeof(ValidationCheck), "IsUniqueEmail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vul je wachtwoord in")]
        [StringLength(100, ErrorMessage = "Het {0} moet op zijn minst {2} tekens lang zijn.", MinimumLength = 6)]
        [RegularExpression(@"^.*(?=.{6})(?=.*\d)(?=.*[a-zA-Z]).*$", ErrorMessage = "Het wachtwoord moet ministens uit een Hoofdletter, kleine letters en een cijfer bestaan")]
        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Herhaal wachtwoord")]
        [Compare("Password", ErrorMessage = "De wachtwoorden komen niet overheen")]
        public string ConfirmPassword { get; set; }
    }

    public enum Gender
    {
        Man = 0,
        Vrouw = 1
    }

    public enum SexualPreference
    {
        Man = 1,
        Vrouw = 0
    }

    public class ValidationCheck
    {
        public static ValidationResult IsUniqueEmail(string mail)
        {
            if (mail == null)
            {
                return null;
            }
            else
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var currentEmail = manager.FindByEmail(mail);

                if (currentEmail == null)
                {
                    return null;
                }
                    string email = currentEmail.ToString();

                    if (email == "")
                    {
                        return ValidationResult.Success;
                    }
                    else
                    {
                        return new ValidationResult("Het e-mailadres is al ingebruikt");
                    }
                }
            }
        
        public static ValidationResult IsEigtheenPlus(DateTime birthday)
        {
            DateTime now = DateTime.Today;
            int age = now.Year - birthday.Year;

            if(age < 18)
            {
                return new ValidationResult("Je moet minimaal 18 jaar en ouder zijn");
            }
            else
            {
                return ValidationResult.Success;
            }

        }

    }


    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "De wachtwoorden komen niet overheen")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
