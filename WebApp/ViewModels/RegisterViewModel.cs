using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class RegisterViewModel
    {

        public IEnumerable<University> Universities { get; set; }
        public bool didRegFail { get; set; } 

        public RegisterViewModel(IEnumerable<University> unis)
        {
            Universities = unis;
        }

    }

    public class WebRegisterViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UniversityId { get; set; }
        public string Email { get; set; }
        public string ConfirmEmail { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public bool ConfirmEmailAndPassword()
        {
            return Email == ConfirmEmail && Password == ConfirmPassword && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
        }
    }
}
