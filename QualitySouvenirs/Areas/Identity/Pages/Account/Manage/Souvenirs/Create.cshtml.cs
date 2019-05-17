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
using QualitySouvenirs.Data;
using QualitySouvenirs.Models;
using QualitySouvenirs.Share;
using QualitySouvenirs.Utilities;

namespace QualitySouvenirs.Areas.Identity.Pages.Account.Manage.Souvenirs
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public CreateModel(ApplicationContext context, IHostingEnvironment environment)
        {
            _context = context;
            _hostingEnvironment = environment;
        }

        public IActionResult OnGet()
        {
            ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "Name");
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "ID", "Name");
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
            public int SupplierID { get; set; }

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

            Souvenir souvenir = new Souvenir();
            souvenir.Name = Input.Name;
            souvenir.Description = Input.Description;
            souvenir.Price = Input.Price;
            souvenir.Popularity = Input.Popularity;
            souvenir.CategoryID = Input.CategoryID;
            souvenir.SupplierID = Input.SupplierID;

            var filePath = MyPath.SouvenirsImage + Guid.NewGuid().ToString() + Input.UploadFile.FileName;
            souvenir.PathOfImage = filePath;

            filePath = new Uri(Path.Join(_hostingEnvironment.WebRootPath, filePath)).LocalPath;
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await Input.UploadFile.CopyToAsync(fileStream);
            }

            _context.Souvenirs.Add(souvenir);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}