using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QualitySouvenirs.Models
{
    public class Order
    {
        public int ID { get; set; }
        public string OrderStatus { get; set; }
        public double SubTotal { get; set; }
        public double GST { get; set; }
        public double GrandTotal { get; set; }
        public DateTime Date { get; set; }
    }
}
