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
            parameters.Add("userId", userId);
            parameters.Add("rsoId", rsoId);
            parameters.Add("accessId", 2);

            using (var con = new MySqlConnection(s))
            {
                con.Execute(sProc, parameters, commandTimeout: 120, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public Rso CreateRso(WebCreateRsoViewModel newRsoInfo)
        {
            var sProc = "makeRso";
            var s = Conf.GetConnectionString("MySqlDatabase");
            var userRepo = new UserRepository(Conf, Conn);

            var mems = newRsoInfo.MemberEmails.Split(';');
            HashSet<string> domains = new HashSet<string>();
            List<User> members = new List<User>();

            for (int i = 0; i < mems.Length; i++)
            {
                mems[i] = mems[i].Trim();

                domains.Add(mems[i].Split('@')[1]);
                var u = userRepo.GetUserByEmail(mems[i]);

                if(u != null)
                {
                    members.Add(u);
                }
                
            }

            domains.Add(newRsoInfo.AdminEmail.Split('@')[1]);
            User adminUser = userRepo.GetUserByEmail(newRsoInfo.AdminEmail);
            

            if (domains.Count > 1 || domains.Count <= 0 || members.Count < 4)
            {
                return null;
            }

            University uni = new UniversityRepository(Conn).GetByDomain(domains.First());

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("n", newRsoInfo.Name);
            parameters.Add("u", uni.Id);
            parameters.Add("d", newRsoInfo.Description);
            parameters.Add("a", adminUser.Id);
            parameters.Add("s", members[0].Id);
            parameters.Add("ss", members[1].Id);
            parameters.Add("sss", members[2].Id);
            parameters.Add("ssss", members[3].Id);
            parameters.Add("r", -1, direction: System.Data.ParameterDirection.InputOutput);

            using (var con = new MySqlConnection(s))
            {
                con.Execute(sProc, parameters, commandTimeout: 120, commandType: System.Data.CommandType.StoredProcedure);

                int? rsoId = parameters.Get<int?>("r");

                if (rsoId is null)
                    return null;
                var rso = GetRsoById(rsoId.Value);


                if (members.Count > 4)
                {
                    for (int i = 4; i < members.Count; i++)
                    {
                        AddUserToRso(members[i], rso.Id);
                    }
                }

                return rso;
            }
        }

    }
}
