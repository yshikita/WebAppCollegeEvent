using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string CategoryType { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int eTypeId { get; set; }
        public int eStatusId { get; set; }
    }

    public class WebEvent
    {
        public string Name { get; set; }
        public DateTime Dt { get; set; }
        public int Dur { get; set; }
        public string Loc { get; set; }
        public decimal Lati { get; set; }
        public decimal Longi { get; set; }
        public string Descr { get; set; }
        public int Cat { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int EventTypeId { get; set; }
    }
}
