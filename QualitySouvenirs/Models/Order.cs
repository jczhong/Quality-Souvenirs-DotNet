using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QualitySouvenirs.Models
{
    public class Order
    {
        public int ID { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public double SubTotal { get; set; }
        public double GST { get; set; }
        public double GrandTotal { get; set; }
        public string OrderStatus { get; set; }
        public DateTime Date { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
