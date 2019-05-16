using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QualitySouvenirs.Data;
using QualitySouvenirs.Models;

namespace QualitySouvenirs.Areas.Identity.Pages.Account.Manage.SouvenirsManagement
{
    public class DetailsModel : PageModel
    {
        private readonly QualitySouvenirs.Data.ApplicationContext _context;

        public DetailsModel(QualitySouvenirs.Data.ApplicationContext context)
        {
            _context = context;
        }

        public Souvenir Souvenir { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Souvenir = await _context.Souvenirs.Include(souvenirs => souvenirs.Category).FirstOrDefaultAsync(m => m.ID == id);

            if (Souvenir == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
