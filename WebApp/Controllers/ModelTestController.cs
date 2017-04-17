using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Contexts;
using WebApp.Models;
using WebApp.Repositories;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class ModelTestController : Controller
    {

        private readonly MySqlContext cont;

        public ModelTestController(MySqlContext context)
        {
            cont = context;
        }

        public IActionResult Index()
        {

            var users = cont.User.ToList();
            
            return View();
        }

    }
}
