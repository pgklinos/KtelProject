namespace KtelProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Driver
    {
        [Key]
        public int DriverID { get; set; }

        [ForeignKey("Bus")]
        public int BusID { get; set; }
        
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public int PhoneNumber { get; set; }

        public virtual Bus Bus { get; set; }
    }
}
