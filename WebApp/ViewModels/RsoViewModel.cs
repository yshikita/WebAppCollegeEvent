using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class RsoViewModel
    {
        public Rso Rso { get; set; }
        public User Admin { get; set; }
        public IEnumerable<UserRso> Members { get; set; }
    }
}
