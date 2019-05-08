using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QualitySouvenirs.Models
{
    public class OrderItem
    {
        public int ID { get; set; }
        public int SouvenirID { get; set; }
        public int OrderID { get; set; }
        public int Quantity { get; set; }
    }
}
