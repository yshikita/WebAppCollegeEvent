using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Event> UpcomingEvents{ get; set; }
        public IEnumerable<WebEvent> Locations { get; set; }
        public User User { get; set; }
    }
}
