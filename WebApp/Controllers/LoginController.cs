using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using WebApp.Contexts;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    [Authorize]
    public class LoginController : BaseController
    {
        private readonly IConfigurationRoot config;
        private MySqlContext context;

        public LoginController(IConfigurationRoot conf, MySqlContext cont)
        {
            config = conf;
            context = cont;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            SetUserData();
            return View();
        }

        [AllowAnonymous]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("User");
            ViewData["User"] = null;
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult CheckCreds(Login log)
        {
            //Verify email/password combination

            //give cookie if login is valid?
            var user = log.VerifyLogin(config, context);
            if (user != null)
            {
                CookieOptions options = new CookieOptions() { Expires = DateTime.Now.AddMinutes(20) };
                Response.Cookies.Append("User", UserSerialization.SerializeUser(user), options);

                ViewData["User"] = user;

                return RedirectToAction("Index", "Home");
            }

            //return back to login page with error message
            return View("Index", true);

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Test()
        {
            SetUserData();

            return View("Pass");
        }
    }
}
