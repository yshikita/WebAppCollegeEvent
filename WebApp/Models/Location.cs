using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
