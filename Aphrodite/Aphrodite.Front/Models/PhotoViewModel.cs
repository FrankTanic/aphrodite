using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aphrodite.Front.Models
{
    public class UserPhoto
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public string File { get; set; }
        public virtual ProfileViewModel Profile { get; set; }
    }

}