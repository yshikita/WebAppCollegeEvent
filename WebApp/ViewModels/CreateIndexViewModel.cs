using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class CreateIndexViewModel
    {
        public User User{ get; set; }
        public bool IsSuperAdmin { get; private set; }

        public CreateIndexViewModel(User user)
        {
            User = user;
            IsSuperAdmin = user.Id == 1 && User.FirstName == "root" && User.LastName == "root"; //The only super admin has these properties

        }
    }
}
