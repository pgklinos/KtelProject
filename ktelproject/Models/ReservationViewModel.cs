using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KtelProject.Models
{
    public class ReservationViewModel
    {


        public int TicketID { get; set; }

        public int PassengerID { get; set; }

        public int RouteID { get; set; }

        public DateTime DateOfReservation { get; set; }

        public string DayOfTrip { get; set; }

        public int PriceOfReservation { get; set; }

        public int SeatsOfReservation { get; set; }


        public string Departure { get; set; }
        public string Destination { get; set; }

        public string TripTime { get; set; }



        public virtual Passenger Passenger { get; set; }

        public virtual Route Route { get; set; }

        public virtual Trip Trip { get; set; }
    }
}

