using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aphrodite.Data.Models
{
    public class Photo
    {
        public int UserID { get; set; }
        public string PhotoDir { get; set; }
        public string CurrentAvatar { get; set; }
        public int PhotoID { get; set; }
        public int Priority { get; set; }

        public virtual User User { get; set; }
    }
}
