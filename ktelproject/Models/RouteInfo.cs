using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KtelProject.Models
{
    public class RouteInfo
    {

        public string SpecificRouteID { get; set; }

        public DateTime RouteDay { get; set; }

        public string RouteTime { get; set; }

        public int BusID { get; set; }

        public int AvailableSeats { get; set; }

        public int Adults { get; set; }
       
        public int Childrens { get; set; }

        public int Unemployed { get; set; }

        public int RouteID{ get; set; }
    }
}