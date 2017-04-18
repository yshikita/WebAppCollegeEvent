using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Contexts;
using WebApp.ViewModels;
using WebApp.Models;
using WebApp.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    public class RegisterController : BaseController
    {
        private readonly IConfigurationRoot config;
        private MySqlContext context;

        public RegisterController(IConfigurationRoot conf, MySqlContext cont)
        {
            context = cont;
            config = conf;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            SetUserData();

            var a = context.University.ToList();
            return View(new RegisterViewModel(a));
        }

        public IActionResult Signup(WebRegisterViewModel newUserInfo)
        {

            //Verify data to be correct
            if (newUserInfo.ConfirmEmailAndPassword())
            {
                //if count is 0 then this email hasnt been used for a previous user
                if (context.User.Where(x => x.Email == newUserInfo.Email).Count() == 0)
                {
                    var userRepo = new UserRepository(config, context);
                    var user = userRepo.CreateUser(newUserInfo);

                    if (user != null)
                    {
                        CookieOptions options = new CookieOptions() { Expires = DateTime.Now.AddMinutes(20) };
                        Response.Cookies.Delete("User");
                        Response.Cookies.Append("User", UserSerialization.SerializeUser(user), options);

                        ViewData["User"] = user;

                        return RedirectToAction("Index","Login");
                    }

                }

            }

            return View("Index", new RegisterViewModel(context.University.ToList()) { didRegFail = true });
        }
    }
}
