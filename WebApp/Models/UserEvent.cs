using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class UserEvent
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
    }
}
