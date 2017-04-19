using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Contexts;
using WebApp.Repositories;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class RsoController : BaseController
    {

        private readonly IConfigurationRoot config;
        private MySqlContext context;

        public RsoController(IConfigurationRoot conf, MySqlContext cont)
        {
            config = conf;
            context = cont;
        }

        public IActionResult Index()
        {
            SetUserData();
            return View(context.Rso.ToList());
        }

        public IActionResult RsoDetails(int Id)
        {
            SetUserData();

            RsoViewModel vm = new RsoViewModel();
            vm.Rso = context.Rso.Where(x => x.Id == Id).FirstOrDefault();
            int adminId = context.UserRso.Where(x => x.AccessLvlId == 1).FirstOrDefault().UserId;
            vm.Admin = context.User.Where(x => x.Id == adminId).FirstOrDefault();
            vm.Members = context.UserRso.Where(x => x.AccessLvlId == 2).ToList();

            return View(vm);
        }

        public IActionResult Join(int Id)
        {
            SetUserData();
            var rsoRepo = new RsoRepository(config, context);
            rsoRepo.AddUserToRso(TheUser, Id);

            return RedirectToAction("RsoDetails/" + Id);
        }
    }
}
