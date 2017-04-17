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
        public IActionResult MakeUniversity(WebUniversity uni)
        {
            //Insert new info into University Table, and the Location Table
            string sProc = "makeUniversity";
            var s = config.GetConnectionString("MySqlDatabase");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("name", uni.Name);
            parameters.Add("acr", uni.Abbrev);
            parameters.Add("numStud", uni.NumStud);
            parameters.Add("descr", uni.Descr);
            parameters.Add("lati", uni.Lati);
            parameters.Add("longi", uni.Longi);


            using (var con = new MySqlConnection(s))
            {
                con.Execute(sProc, parameters, commandTimeout: 120, commandType: System.Data.CommandType.StoredProcedure);
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult MakeEvent(WebEvent e)
        {
            string sProc = "makeEvent";
            var s = config.GetConnectionString("MySqlDatabase");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("name", e.Name);
            parameters.Add("theDate", e.Dt);
            parameters.Add("duration", e.Dur);
            parameters.Add("locName", e.Loc);
            parameters.Add("catId", e.Cat);
            parameters.Add("descr", e.Descr);
            parameters.Add("phone", e.Phone);
            parameters.Add("email", e.Email);
            parameters.Add("eTypeId", e.EventTypeId);
            parameters.Add("lati", e.Lati);
            parameters.Add("longi", e.Longi);

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
