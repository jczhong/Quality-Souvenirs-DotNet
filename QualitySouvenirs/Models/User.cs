using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace QualitySouvenirs.Models
{
    public class User
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Please confirm your password.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
