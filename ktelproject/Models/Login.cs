using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KtelProject.Models
{
    public class Login
    {
        [Key]
        [Required(ErrorMessage ="A Valid Email is Required")]
        [EmailAddress]
        [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"+ "@"+ @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$")]
        public string Email { get; set; }

        [Required(ErrorMessage ="a Password is Required")]
        [DataType(DataType.Password)]
        //password at least 6, at max 15, one lower, one upper,one number
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{6,15}$")]  
        public string Password { get; set; }
        
    }
}