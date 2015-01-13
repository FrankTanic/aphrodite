using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aphrodite.Front.Models
{
    public class MatchViewModel
    {
        public int ID { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public int Approve { get; set; }
    }
    public class matches
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}