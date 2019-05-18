using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QualitySouvenirs.Models
{
    public class Souvenir
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        public double Price { get; set; }
        public int Popularity { get; set; }
        public string PathOfImage { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
        public int SupplierID { get; set; }
        public Supplier Supplier { get; set; }
    }
}
