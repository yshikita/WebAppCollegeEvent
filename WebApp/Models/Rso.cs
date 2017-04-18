using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Rso
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class WebRso
    {
        public string Name { get; set; }
        public string AdEmail { get; set; }
        public int NumMemb { get; set; }
        public string Memb { get; set; }
        public string Descr { get; set; }
        public int UniversityId { get; set; }
    }
}
