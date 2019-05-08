using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QualitySouvenirs.Models
{
    public class ShoppingCart
    {
        public string ID { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
