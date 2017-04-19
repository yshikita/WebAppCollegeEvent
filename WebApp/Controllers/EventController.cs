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
    public class EventController : BaseController
    {

        private readonly IConfigurationRoot config;
        private MySqlContext context;

        public EventController(IConfigurationRoot conf, MySqlContext cont)
        {
            config = conf;
            context = cont;
        }

        public IActionResult Index()
        {
            SetUserData();
            return View(context.Event.ToList());
        }

        public IActionResult EventDetails(int Id)
        {
            SetUserData();

            var e = context.Event.Where(x => x.Id == Id).FirstOrDefault();
            var r = context.UserEvent.Where(x => x.EventId == e.Id).ToList();

            return View(new EventViewModel() { Event = e, Reviews = r });
        }

        public IActionResult CommentReview(int Rating, string Comment, int EventId)
        {
            SetUserData();

            string sProc = "makeEventReview";
            var s = config.GetConnectionString("MySqlDatabase");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("uId", TheUser.Id);
            parameters.Add("eID", EventId);
            parameters.Add("comm", Comment);
            parameters.Add("rat", Rating);

            using (var con = new MySqlConnection(s))
            {
                con.Execute(sProc, parameters, commandTimeout: 120, commandType: System.Data.CommandType.StoredProcedure);
            }

            return RedirectToAction("EventDetails/" + EventId);
        }

    }
}
