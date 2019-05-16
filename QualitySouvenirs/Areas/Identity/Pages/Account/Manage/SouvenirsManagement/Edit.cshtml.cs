using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QualitySouvenirs.Data;
using QualitySouvenirs.Models;
using QualitySouvenirs.Share;
using QualitySouvenirs.Utilities;

namespace QualitySouvenirs.Areas.Identity.Pages.Account.Manage.SouvenirsManagement
{
    public class EditModel : PageModel
    {
        private readonly QualitySouvenirs.Data.ApplicationContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public EditModel(QualitySouvenirs.Data.ApplicationContext context, IHostingEnvironment environment)
        {
            _context = context;
            _hostingEnvironment = environment;
        }

        public class InputModel
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public double Price { get; set; }
            public int Popularity { get; set; }
            public int CategoryID { get; set; }

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

            var souvenir = await _context.Souvenirs.Include(souvenirs => souvenirs.Category).FirstOrDefaultAsync(m => m.ID == id);

            if (souvenir == null)
            {
                return NotFound();
            }

            Input = new InputModel
            {
                ID = souvenir.ID,
                Name = souvenir.Name,
                Description = souvenir.Description,
                Price = souvenir.Price,
                Popularity = souvenir.Popularity,
                CategoryID = souvenir.CategoryID,
            };


            PopulateCategoriesDropDownList();

            return Page();
        }

        private void PopulateCategoriesDropDownList(object selectedCategory = null)
        {
            var categoriesQuery = from d in _context.Categories
                                  orderby d.Name
                                  select d;
            ViewData["CategoryID"] = new SelectList(categoriesQuery.AsNoTracking(), "ID", "Name", selectedCategory);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var id = Input.ID;

            var souvenir = await _context.Souvenirs.FirstOrDefaultAsync(m => m.ID == id);

            if (null == souvenir)
            {
                return Page();
            }

            souvenir.Name = Input.Name;
            souvenir.Description = Input.Description;
            souvenir.Price = Input.Price;
            souvenir.Popularity = Input.Popularity;
            souvenir.CategoryID = Input.CategoryID;

            if(null != Input.UploadFile)
            {
                if (FileHelpers.ProcessImageFormFile(Input.UploadFile, ModelState) == false)
                {
                    return Page();
                }

                var filePath = new Uri(Path.Join(_hostingEnvironment.WebRootPath, souvenir.PathOfImage)).LocalPath;
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);

                    filePath = MyPath.SouvenirsImage + Guid.NewGuid().ToString() + Input.UploadFile.FileName;
                    souvenir.PathOfImage = filePath;

                    filePath = new Uri(Path.Join(_hostingEnvironment.WebRootPath, filePath)).LocalPath;
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await Input.UploadFile.CopyToAsync(fileStream);
                    }
                }
            }


            _context.Attach(souvenir).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SouvenirExists(Input.ID))
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

        private bool SouvenirExists(int id)
        {
            return _context.Souvenirs.Any(e => e.ID == id);
        }
    }
}
