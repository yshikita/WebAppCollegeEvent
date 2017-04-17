using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class UserUniversity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int UniversityId { get; set; }
        public int AccessLvlId { get; set; }
    }
}
