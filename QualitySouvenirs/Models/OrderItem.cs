using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QualitySouvenirs.Models
{
    public class OrderItem
    {
        public int ID { get; set; }
        public Order Order { get; set; }
        public double ItemPrice { get; set; }
        public Souvenir Souvenir { get; set; }
        public int Quantity { get; set; }
    }
}
