using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Contexts;
using WebApp.Repositories;

namespace WebApp.Models
{
    public class Login
    {
        public string Email { get; set; }
        public string Password { get; set; }
        
        public User VerifyLogin(IConfigurationRoot conf, MySqlContext cont)
        {
            //make db connection and check if user and password match

            var sProc = "validateUser";

            /*
             * "MySqlDatabase": "Server=31.220.105.201;Port=3306;Database=nanahsco_CollegeWeb;Uid=nanahsco_admin;Pwd=DatabaseSystems!;"
             */
            //var s = conf.GetConnectionString("MySqlDatabase");
            string s = conf.GetConnectionString("MySqlDatabase");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("e", Email);
            parameters.Add("p", Password);
            parameters.Add("u", 0, direction: System.Data.ParameterDirection.Output);

            using (var con = new MySqlConnection(s))
            {
                con.Execute(sProc, parameters, commandTimeout: 120, commandType: System.Data.CommandType.StoredProcedure);

                int? userId = parameters.Get<int?>("u");

                UserRepository repo = new UserRepository(cont);

                if (userId is null)
                    return null;
                var user = repo.GetUserById(userId.Value);

                return user;
            }
            
        }

        public override string ToString()
        {
            return Email + ";" + Password;
        }
    }
    
}
