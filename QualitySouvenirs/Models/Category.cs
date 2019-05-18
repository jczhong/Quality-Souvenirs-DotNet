using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QualitySouvenirs.Models
{
    public class Category
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string PathOfImage { get; set; }
        public ICollection<Souvenir> Souvenirs { get; set; }
    }
}
