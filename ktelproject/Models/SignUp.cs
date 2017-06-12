using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;



namespace KtelProject.Models
{
    public class SignUp
    {
        [Key]
        [Required]
        [EmailAddress]
        [StringLength(150)]
        [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$")]
        [Display(Name = "Email Address: ")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6)]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{6,15}$")]
        [Display(Name = "Password: ")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "First Name: ")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name: ")]
        public string LastName { get; set; }

        [Display(Name = "Phone Number")]
        public int PhoneNumber { get; set; }


    }
    
}