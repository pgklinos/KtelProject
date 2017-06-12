namespace KtelProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Trip
    {
        public int TripID { get; set; }

        public int RouteID { get; set; }

        [Column("TripTime")]
        [Required]
        public string TripTime { get; set; }

        public int BusID { get; set; }

        public virtual Bus Bus { get; set; }

        public virtual Route Route { get; set; }
    }
}
