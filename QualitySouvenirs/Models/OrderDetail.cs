using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QualitySouvenirs.Models
{
    public class OrderDetail
    {
        public int ID { get; set; }
        public int Quantity { get; set; }
        public Souvenir Souvenir { get; set; }
        public Order Order { get; set; }
    }
}
