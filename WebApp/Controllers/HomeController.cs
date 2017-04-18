using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Contexts;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {

        MySqlContext cont { get; set; }
        User TheUser { get; set; }

        public HomeController(MySqlContext context)
        {
            cont = context;
            
        }

        public IActionResult Index()
        {
            SetUserData();
            HomeViewModel vm = new HomeViewModel() { User = TheUser, UpcomingEvents = cont.Event.Where(x => x.Date > DateTime.Now && x.Date < DateTime.Now.AddDays(7)).ToList() };

            

            return View(vm);
        }

        public IActionResult FindEvents()
        {
            SetUserData();
            ViewData["Message"] = "Find Events Page";

            return View();
        }

        public IActionResult Contact()
        {
            SetUserData();
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            SetUserData();
            return View();
        }

        private void SetUserData()
        {
            TheUser = UserSerialization.DeserializeUser(Request.Cookies["User"]);
            ViewData["User"] = TheUser;
        }
    }
}
