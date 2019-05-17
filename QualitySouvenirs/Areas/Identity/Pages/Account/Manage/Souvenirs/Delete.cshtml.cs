using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QualitySouvenirs.Data;
using QualitySouvenirs.Models;

namespace QualitySouvenirs.Areas.Identity.Pages.Account.Manage.Souvenirs
{
    public class DeleteModel : PageModel
    {
        private readonly QualitySouvenirs.Data.ApplicationContext _context;

        public DeleteModel(QualitySouvenirs.Data.ApplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Souvenir Souvenir { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Souvenir = await _context.Souvenirs
                .Include(s => s.Category)
                .Include(s => s.Supplier).FirstOrDefaultAsync(m => m.ID == id);

            if (Souvenir == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Souvenir = await _context.Souvenirs.FindAsync(id);

            if (Souvenir != null)
            {
                _context.Souvenirs.Remove(Souvenir);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
