using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QualitySouvenirs.Models
{
    public class SortViewModel
    {
        public string SortType { get; set; }

        public List<SelectListItem> SortTypes { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "popularity", Text = "Popularity High to Low" },
            new SelectListItem { Value = "popularity_desc", Text = "Popularity Low to High" },
            new SelectListItem { Value = "price_desc", Text = "Price Low to High"  },
            new SelectListItem { Value = "price", Text = "Price High to Low"  },
        };
    }
}
