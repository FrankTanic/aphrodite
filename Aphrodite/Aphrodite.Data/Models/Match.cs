using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aphrodite.Data.Models
{
    public class Match
    {
        public int MatchID { get; set; }
        public string Pending { get; set; }
        public string Matches { get; set; }
        public int UserIDFrom { get; set; }
        public int UserIDTo { get; set; }

        public virtual User User { get; set; }
    }
}
