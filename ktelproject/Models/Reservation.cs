namespace KtelProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Reservation")]
    public partial class Reservation
    {
        [Key]
        public int TicketID { get; set; }

        public int PassengerID { get; set; }

        public int RouteID { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOfReservation { get; set; }

        public string DayOfTrip { get; set; }

 
        public int PriceOfReservation { get; set; }

        public int  SeatsOfReservation { get; set; }

        public string DateTimeOfReservation { get; set; }



        public virtual Passenger Passenger { get; set; }

        public virtual Route Route { get; set; }
    }
}
