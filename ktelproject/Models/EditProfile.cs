using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;



namespace KtelProject.Models
{
    public class EditProfile

    {
   
        [Display(Name = "First Name: ")]
        public string FirstName { get; set; }


        [Display(Name = "Last Name: ")]
        public string LastName { get; set; }

        [Display(Name = "Phone Number: ")]
        public int PhoneNumber { get; set; }

    }

}