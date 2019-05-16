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
    public class CreateModel : PageModel
    {
        private readonly QualitySouvenirs.Data.ApplicationContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public CreateModel(QualitySouvenirs.Data.ApplicationContext context, IHostingEnvironment environment)
        {
            _context = context;
            _hostingEnvironment = environment;
        }

        private void PopulateCategoriesDropDownList(object selectedCategory = null)
        {
            var categoriesQuery = from d in _context.Categories
                                  orderby d.Name
                                  select d;
            ViewData["CategoryID"] = new SelectList(categoriesQuery.AsNoTracking(), "ID", "Name", selectedCategory);
        }

        public IActionResult OnGet()
        {
            PopulateCategoriesDropDownList();
            return Page();
        }


        public class InputModel
        {
            [Required]
            public string Name { get; set; }
            [Required]
            public string Description { get; set; }
            [Required]
            public double Price { get; set; }
            [Required]
            public int Popularity { get; set; }
            [Required]
            public int CategoryID { get; set; }

            [Required]
            [Display(Name = "Image File")]
            public IFormFile UploadFile { get; set; }
        }


        [BindProperty]
        public InputModel Input { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (FileHelpers.ProcessImageFormFile(Input.UploadFile, ModelState) == false)
            {
                return Page();
            }

            Souvenir su = new Souvenir();
            su.Name = Input.Name;
            su.Description = Input.Description;
            su.Price = Input.Price;
            su.Popularity = Input.Popularity;
            su.CategoryID = Input.CategoryID;

            var filePath = MyPath.SouvenirsImage + Guid.NewGuid().ToString() + Input.UploadFile.FileName;
            su.PathOfImage = filePath;

            filePath = new Uri(Path.Join(_hostingEnvironment.WebRootPath, filePath)).LocalPath;
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await Input.UploadFile.CopyToAsync(fileStream);
            }

            _context.Souvenirs.Add(su);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}