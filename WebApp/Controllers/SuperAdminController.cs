using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Contexts;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class SuperAdminController : BaseController
    {

        private readonly IConfigurationRoot config;
        private MySqlContext context;

        public SuperAdminController(IConfigurationRoot conf, MySqlContext cont)
        {
            config = conf;
            context = cont;
        }

        public IActionResult Index()
        {
            SetUserData();

            var s = isSuperAdmin();

            if (!s)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public IActionResult Events()
        {
            SetUserData();
            if (!isSuperAdmin())
            {
                return RedirectToAction("Index", "Home");
            }

            return View(context.Event.ToList());
        }

        public IActionResult Rsos()
        {
            SetUserData();
            if (!isSuperAdmin())
            {
                return RedirectToAction("Index", "Home");
            }

            return View(context.Rso.ToList());
        }

        public IActionResult Students()
        {
            SetUserData();
            if (!isSuperAdmin())
            {
                return RedirectToAction("Index", "Home");
            }

            return View(context.User.Where(x => x.Id != 1).ToList());
        }

        public IActionResult Universities()
        {
            SetUserData();
            if (!isSuperAdmin())
            {
                return RedirectToAction("Index", "Home");
            }

            return View(context.University.ToList());
        }

        public IActionResult Approve(int Id)
        {
            SetUserData();
            if (!isSuperAdmin())
            {
                return RedirectToAction("Index", "Home");
            }

            //approve event id
            string sProc = "eventApprove";
            var s = config.GetConnectionString("MySqlDatabase");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("rsoId", Id);

            using (var con = new MySqlConnection(s))
            {
                con.Execute(sProc, parameters, commandTimeout: 120, commandType: System.Data.CommandType.StoredProcedure);
            }

            return View("Events", context.Event.ToList());
        }

        public IActionResult Deny(int Id)
        {
            SetUserData();
            if (!isSuperAdmin())
            {
                return RedirectToAction("Index", "Home");
            }

            //approve event id
            string sProc = "eventDeny";
            var s = config.GetConnectionString("MySqlDatabase");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("rsoId", Id);

            using (var con = new MySqlConnection(s))
            {
                con.Execute(sProc, parameters, commandTimeout: 120, commandType: System.Data.CommandType.StoredProcedure);
            }

            return View("Events", context.Event.ToList());
        }

        private bool isSuperAdmin()
        {
            if(TheUser == null)
            {
                return false;
            }

            return TheUser.Id == 1 && TheUser.FirstName == "root" && TheUser.LastName == "root" && TheUser.Email == "root@root.com";
        }
    }
}
