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
    public class LoginController : Controller
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
            return View();
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

                return View("Pass");
            }

            //return back to login page with error message
            return View("Index", true);

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Test()
        {

            User user = new User();
            var c = Request.Cookies["User"].Split('~');

            user.Id = int.Parse(c[0]);
            user.FirstName = c[0];
            user.LastName = c[0];
            user.Email = c[0];
            user.Password = c[0];
            


            return View("Pass");
        }
    }
}
