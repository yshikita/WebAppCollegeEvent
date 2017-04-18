using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class BaseController : Controller
    {

        public User TheUser { get; set; }

        public void SetUserData()
        {
            TheUser = UserSerialization.DeserializeUser(Request.Cookies["User"]);
            ViewData["User"] = TheUser;
        }
    }
}
