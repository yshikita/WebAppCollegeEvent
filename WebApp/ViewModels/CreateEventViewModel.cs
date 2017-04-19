using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class CreateEventViewModel
    {
        public IEnumerable<Category> EventCategories { get; set; }
        public User User { get; set; }
        public University University { get; set; }
        public IEnumerable<Rso> Rsos{ get; set; }
        public IEnumerable<EventType> EventTypes { get; set; }
    }
    
}
