﻿using System;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Aphrodite.Front.Models
{
    public class IndexViewModel
    {
        public string Email { get; set; }
        public string Photo { get; set; }
        public DateTime BirthDay { get; set; }
        public ApplicationUser User { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class EditViewModel
    {
        [Required(ErrorMessage = "Vul je voornaam in")]
        [StringLength(255, ErrorMessage = "Je voornaam naam moet uit minimaal 2 tekens bestaan en mag maximaal 255 tekens bevatten", MinimumLength = 2)]
        [Display(Name = "Voornaam")]
        public string DisplayName { get; set; }

        [Required(ErrorMessage = "Vul je e-mailadres in")]
        [EmailAddress(ErrorMessage = "Dit is geen geldig e-mailadres")]
        [Display(Name = "E-mailadres")]
        public string Email { get; set; }

        public Gender Gender { get; set; }

        [Display(Name = "Seksuele voorkeur")]
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
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Vul je huidige wachtwoord in")]
        [DataType(DataType.Password)]
        [Display(Name = "Huidig wachtwoord")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Vul je nieuwe wachtwoord in")]
        [StringLength(100, ErrorMessage = "Het {0} moet op zijn minst {2} tekens lang zijn.", MinimumLength = 6)]
        [RegularExpression(@"^.*(?=.{6})(?=.*[A-Z])(?=.*\d)(?=.*[a-zA-Z]).*$", ErrorMessage = "Het wachtwoord moet minstens uit een hoofdletter, kleine letter, een cijfer en die bestaan uit minimaal 6 tekens")]
        [DataType(DataType.Password)]
        [Display(Name = "Nieuw wachtwoord")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Herhaal wachtwoord")]
        [Compare("NewPassword", ErrorMessage = "De wachtwoorden komen niet overheen")]
        public string ConfirmPassword { get; set; }
    }
}