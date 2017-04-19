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
    public class HomeController : BaseController
    {

        MySqlContext Cont { get; set; }

        public HomeController(MySqlContext context)
        {
            Cont = context;
            
        }

        public IActionResult Index()
        {
            SetUserData();
            HomeViewModel vm = new HomeViewModel() { User = TheUser, UpcomingEvents = Cont.Event.Where(x => x.Date > DateTime.Now && x.Date < DateTime.Now.AddDays(7) && x.eStatusId == 1).ToList() };

            

            return View(vm);
        }

        public IActionResult FindEvents()
        {
            SetUserData();
            ViewData["Message"] = "Find Events Page";

            return View();
        }

        public IActionResult About()
        {
            SetUserData();
            ViewData["Message"] = "Your About page.";

            return View();
        }

        public IActionResult Error()
        {
            SetUserData();
            return View();
        }
        
    }
}
