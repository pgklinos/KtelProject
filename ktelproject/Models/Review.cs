namespace KtelProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Review")]
    public partial class Review
    {
        [Key]
        public int ReviewID { get; set; }

        [ForeignKey("Passenger")]
        public int PassengerID { get; set; }

        [ForeignKey("Driver")]
        public int DriverID { get; set; }

        public int Rating { get; set; }

        [Required]
        public string Comment { get; set; }

        public virtual Passenger Passenger { get; set; }

        public virtual Driver Driver  { get; set; }
    }
}
