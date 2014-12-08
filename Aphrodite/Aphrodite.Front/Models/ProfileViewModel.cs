using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aphrodite.Front.Models
{
    public class ProfileViewModel
    {
        public string ID { get; set; }
        public string DisplayName { get; set; }
        public DateTime Birthday { get; set; }
        public virtual ICollection<UserPhoto> Photo { get; set; }
    }
}