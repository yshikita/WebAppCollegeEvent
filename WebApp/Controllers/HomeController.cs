using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Contexts;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {

        MySqlContext cont { get; set; }

        public HomeController(MySqlContext context)
        {
            cont = context;
        }

        public IActionResult Index()
        {
            return View(cont.Event.ToList());
            //return View();
        }

        public IActionResult FindEvents()
        {
            ViewData["Message"] = "Find Events Page";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
