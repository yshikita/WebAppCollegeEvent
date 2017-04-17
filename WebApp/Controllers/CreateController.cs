using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using WebApp.Contexts;
using Dapper;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;

namespace WebApp.Controllers
{
    public class CreateController : Controller
    {

        private readonly IConfigurationRoot config;
        private MySqlContext context;

        public CreateController(IConfigurationRoot conf, MySqlContext cont)
        {
            config = conf;
            context = cont;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Rso()
        {
            return View();
        }
        
        public IActionResult University()
        {

            return View();
        }

        public IActionResult Event()
        {
            return View(context.Category.Distinct());
        }

        [HttpPost]
        public IActionResult MakeUniversity(University uni, Location loc)
        {
            //Insert new info into University Table, and the Location Table
            string sProc = "makeUniversity";
            var s = config.GetConnectionString("MySqlDatabase");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("name", uni.Name);
            parameters.Add("acr", uni.Acronim);
            parameters.Add("numStud", uni.NumberOfStudents);
            parameters.Add("descr", uni.Description);
            parameters.Add("lati", loc.Latitude);
            parameters.Add("longi", loc.Longitude);


            using (var con = new MySqlConnection(s))
            {
                con.Execute(sProc, parameters, commandTimeout: 120, commandType: System.Data.CommandType.StoredProcedure);
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult MakeEvent(Event e, Location loc)
        {
            string sProc = "makeEvent";
            var s = config.GetConnectionString("MySqlDatabase");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("name", e.Name);
            parameters.Add("theDate", e.Date);
            parameters.Add("duration", e.Duration);
            parameters.Add("locName", loc.LocationName);
            parameters.Add("catId", e.CategoryId);
            parameters.Add("descr", e.Description);
            parameters.Add("phone", e.Phone);
            parameters.Add("email", e.email);
            parameters.Add("eTypeId", e.eTypeId);
            parameters.Add("lati", loc.Latitude);
            parameters.Add("longi", loc.Longitude);

            using (var con = new MySqlConnection(s))
            {
                con.Execute(sProc, parameters, commandTimeout: 120, commandType: System.Data.CommandType.StoredProcedure);
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult MakeRso(Rso rso)
        {

            return Ok();
        }
    }
}
