namespace KtelProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Passenger
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Passenger()
        {
            Reservations = new HashSet<Reservation>();
            Reviews = new HashSet<Review>();
        }

        [Key]
        public int PassengerID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required(ErrorMessage ="A valid email is required to login")]
        public string Email { get; set; }

        [Required(ErrorMessage = " A valid password must be entered to login")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public int PhoneNumber { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservation> Reservations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
