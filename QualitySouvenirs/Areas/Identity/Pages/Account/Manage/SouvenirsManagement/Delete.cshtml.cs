using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QualitySouvenirs.Data;
using QualitySouvenirs.Models;

namespace QualitySouvenirs.Areas.Identity.Pages.Account.Manage.SouvenirsManagement
{
    public class DeleteModel : PageModel
    {
        private readonly QualitySouvenirs.Data.ApplicationContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public DeleteModel(QualitySouvenirs.Data.ApplicationContext context, IHostingEnvironment environment)
        {
            _context = context;
            _hostingEnvironment = environment;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Souvenir = await _context.Souvenirs.FindAsync(id);

            if (Souvenir != null)
            {
                var filePath = new Uri(Path.Join(_hostingEnvironment.WebRootPath, Souvenir.PathOfImage)).LocalPath;
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                _context.Souvenirs.Remove(Souvenir);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
