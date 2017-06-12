namespace KtelProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Capacity")]
    public partial class Capacity
    {
        public int CapacityID { get; set; }

        [Column("SpecificRouteID")]
        [Required]
        public string SpecificRouteID { get; set; }

        [Required]
        public DateTime RouteDay { get; set; }

        [Required]
        public string RouteTime { get; set; }

        public int BusID { get; set; }

        public int AvailableSeats { get; set; }

        public virtual Bus Bus { get; set; }
    }
}
