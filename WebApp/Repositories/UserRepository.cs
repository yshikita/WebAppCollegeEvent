using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Contexts;
using WebApp.Models;
using WebApp.ViewModels;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace WebApp.Repositories
{
    public class UserRepository
    {

        public MySqlContext Conn { get; set; }
        public IConfigurationRoot Conf { get; private set; }

        public UserRepository(IConfigurationRoot conf, MySqlContext dbConn)
        {
            Conn = dbConn;
            Conf = conf;
        }

        public IEnumerable<User> AllUsers()
        {
            return Conn.User.ToList();
        }

        public User GetUserById(int userId)
        {
            return Conn.User.Single(x => x.Id == userId);
        }

        public User GetUserByEmail(string email)
        {
            return Conn.User.Where(x => x.Email == email).FirstOrDefault();
        }

        public User CreateUser(WebRegisterViewModel newUserInfo)
        {
            var sProc = "makeUser";
            var s = Conf.GetConnectionString("MySqlDatabase");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("f", newUserInfo.FirstName);
            parameters.Add("l", newUserInfo.LastName);
            parameters.Add("e", newUserInfo.Email);
            parameters.Add("p", newUserInfo.Password);
            parameters.Add("s", 1);
            parameters.Add("u", 0, direction: System.Data.ParameterDirection.Output);

            using (var con = new MySqlConnection(s))
            {
                con.Execute(sProc, parameters, commandTimeout: 120, commandType: System.Data.CommandType.StoredProcedure);

                int? userId = parameters.Get<int?>("u");

                if (userId is null)
                    return null;
                var user = GetUserById(userId.Value);

                return user;
            }
        }

    }
}
