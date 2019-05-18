using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QualitySouvenirs.Data;
using QualitySouvenirs.Share;
using QualitySouvenirs.Utilities;

namespace QualitySouvenirs.Areas.Identity.Pages.Account.Manage.Categories
{
    public class EditModel : PageModel
    {
        private readonly ApplicationContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public EditModel(ApplicationContext context, IHostingEnvironment environment)
        {
            _context = context;
            _hostingEnvironment = environment;
        }

        public class InputModel
        {
            public int ID { get; set; }

            [Required]
            public string Name { get; set; }

            [Display(Name = "Image File")]
            public IFormFile UploadFile { get; set; }
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FirstOrDefaultAsync(m => m.ID == id);

            if (category == null)
            {
                return NotFound();
            }

            Input = new InputModel
            {
                ID = category.ID,
                Name = category.Name
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var category = await _context.Categories.FirstOrDefaultAsync(m => m.ID == Input.ID);
            category.Name = Input.Name;

            if (Input.UploadFile != null)
            {
                if (FileHelpers.ProcessImageFormFile(Input.UploadFile, ModelState) == false)
                {
                    return Page();
                }

                var filePath = new Uri(Path.Join(_hostingEnvironment.WebRootPath, category.PathOfImage)).LocalPath;
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                filePath = MyPath.CategoryImage + Guid.NewGuid().ToString() + Input.UploadFile.FileName;
                category.PathOfImage = filePath;

                filePath = new Uri(Path.Join(_hostingEnvironment.WebRootPath, filePath)).LocalPath;
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await Input.UploadFile.CopyToAsync(fileStream);
                }
            }

            _context.Attach(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(category.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.ID == id);
        }
    }
}
