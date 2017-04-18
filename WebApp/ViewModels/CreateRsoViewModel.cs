using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels
{
    public class CreateRsoViewModel
    {
        public string Name { get; set; }
        public string AdminEmail { get; set; }
        public string MemberEmails { get; set; }
        public string Description { get; set; }
        public int UniversityId { get; set; }
    }
}
