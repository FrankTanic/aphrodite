using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Aphrodite.Front.Models;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Aphrodite.Front.Models
{
    public class ProfileViewModel
    {
        public string ID { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public int Years { get; set; }
        public Gender Gender { get; set; }
        public SexualPreference SexualPreference { get; set; }
        public string Photo { get; set; }
    }
}