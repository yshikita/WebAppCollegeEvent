using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class EventViewModel
    {
        public Event Event { get; set; }
        public IEnumerable<UserEvent> Reviews { get; set; }
    }
}
