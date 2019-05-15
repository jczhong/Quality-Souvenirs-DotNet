using Microsoft.AspNetCore.Mvc.Rendering;
using QualitySouvenirs.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QualitySouvenirs.Models
{
    public class OrderStatusViewModel
    {
        public string Status { get; set; }

        public List<SelectListItem> Statuses { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = OrderStatus.Waiting, Text = "Waiting" },
            new SelectListItem { Value = OrderStatus.Shipped, Text = "Shipped" },
        };
    }
}
