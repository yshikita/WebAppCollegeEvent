using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebApp.Models
{
    public static class UserSerialization
    {
        public static string SerializeUser(User user)
        {
            return JsonConvert.SerializeObject(user);
        }

        public static User DeserializeUser(string json)
        {
            return JsonConvert.DeserializeObject<User>(json);
        }
    }
}
