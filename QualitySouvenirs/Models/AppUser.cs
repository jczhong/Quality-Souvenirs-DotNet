using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace QualitySouvenirs.Models
{
    public class AppUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; }
        public string Address { get; set; }

        [Display(Name ="Status")]
        public bool Enabled { get; set; }
    }
}
