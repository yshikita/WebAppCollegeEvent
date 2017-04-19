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

            UserRepository userRepo = new UserRepository(config, context);

            RsoViewModel vm = new RsoViewModel();
            vm.Rso = context.Rso.Where(x => x.Id == Id).FirstOrDefault();
            int adminId = context.UserRso.Where(x => x.AccessLvlId == 1).FirstOrDefault().UserId;
            vm.Admin = userRepo.GetUserById(adminId);
            var memberIds = context.UserRso.Where(x => x.RsoId == Id).Select(x => x.UserId).ToList();
            vm.Members = userRepo.GetUsersByIds(memberIds);

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
