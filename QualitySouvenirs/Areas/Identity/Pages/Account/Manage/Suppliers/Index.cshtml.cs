﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QualitySouvenirs.Data;
using QualitySouvenirs.Models;

namespace QualitySouvenirs.Areas.Identity.Pages.Account.Manage.Suppliers
{
    public class IndexModel : PageModel
    {
        private readonly QualitySouvenirs.Data.ApplicationContext _context;

        public IndexModel(QualitySouvenirs.Data.ApplicationContext context)
        {
            _context = context;
        }

        public IList<Supplier> Supplier { get;set; }

        public async Task OnGetAsync()
        {
            Supplier = await _context.Suppliers.ToListAsync();
        }
    }
}
