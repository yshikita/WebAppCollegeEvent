using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Contexts;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Repositories
{
    public class RsoRepository
    {

        public MySqlContext Conn { get; set; }
        public IConfigurationRoot Conf { get; private set; }

        public RsoRepository(IConfigurationRoot conf, MySqlContext dbConn)
        {
            Conn = dbConn;
            Conf = conf;
        }

        public IEnumerable<Rso> AllRsos()
        {
            return Conn.Rso.ToList();
        }

        public Rso GetRsoById(int rsoId)
        {
            return Conn.Rso.Single(x => x.Id == rsoId);
        }

        public void AddUserToRso(User user, int rsoId)
        {
            AddUserToRso(user.Id, rsoId);
        }

        public void AddUserToRso(int userId, int rsoId)
        {
            var sProc = "addUserToRso";
            var s = Conf.GetConnectionString("MySqlDatabase");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("u", userId);
            parameters.Add("r", rsoId);
            parameters.Add("a", 2);
        }

        public Rso CreateRso(CreateRsoViewModel newRsoInfo)
        {
            var sProc = "makeRso";
            var s = Conf.GetConnectionString("MySqlDatabase");

            var mems = newRsoInfo.MemberEmails.Split(';');

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("n", newRsoInfo.Name);
            parameters.Add("u", newRsoInfo.UniversityId);
            parameters.Add("d", newRsoInfo.Description);
            parameters.Add("a", newRsoInfo.AdminEmail);
            parameters.Add("s", mems[0]);
            parameters.Add("ss", mems[1]);
            parameters.Add("sss", mems[2]);
            parameters.Add("ssss", mems[3]);
            parameters.Add("r", -1, direction: System.Data.ParameterDirection.InputOutput);

            using (var con = new MySqlConnection(s))
            {
                con.Execute(sProc, parameters, commandTimeout: 120, commandType: System.Data.CommandType.StoredProcedure);

                int? rsoId = parameters.Get<int?>("r");

                if (rsoId is null)
                    return null;
                var rso = GetRsoById(rsoId.Value);


                if(mems.Length > 3)
                {
                    var userRepo = new UserRepository(Conf, Conn);
                    for(int i = 3; i < mems.Length; i++)
                    {
                        AddUserToRso(userRepo.GetUserByEmail(mems[i]), rso.Id);
                    }
                }

                return rso;
            }
        }

    }
}
