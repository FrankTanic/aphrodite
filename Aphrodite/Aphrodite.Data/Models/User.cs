using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aphrodite.Data.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public int Sex { get; set; }
        public int SexualPreference { get; set; }
        public int Age { get; set; }
        public string Location { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset LastLogin { get; set; }

        public virtual ICollection<Match> Matches { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
    }
}
