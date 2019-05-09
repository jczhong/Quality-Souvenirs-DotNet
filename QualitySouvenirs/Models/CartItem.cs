using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QualitySouvenirs.Models
{
    public class CartItem
    {
        public int ID { get; set; }
        public string CartID { get; set; }
        public int Count { get; set; }
        public Souvenir Souvenir { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
