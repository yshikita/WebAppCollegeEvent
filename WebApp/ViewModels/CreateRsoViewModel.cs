using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class CreateRsoViewModel
    {
        public IEnumerable<Category> Categories{ get; set; }
        public int MyProperty { get; set; }
    }

    public class WebCreateRsoViewModel
    {
        public string Name { get; set; }
        public string AdminEmail { get; set; }
        public string MemberEmails { get; set; }
        public string Description { get; set; }
    }
}
