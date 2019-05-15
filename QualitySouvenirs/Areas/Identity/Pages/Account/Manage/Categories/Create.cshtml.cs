using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QualitySouvenirs.Data;
using QualitySouvenirs.Models;
using QualitySouvenirs.Share;
using QualitySouvenirs.Utilities;

namespace QualitySouvenirs.Areas.Identity.Pages.Account.Manage.Categories
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
            return Page();
        }

        public class InputModel
        {
            [Required]
            public string Name { get; set; }

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

            Category category = new Category();
            category.Name = Input.Name;

            var filePath = MyPath.CategoryImage + Guid.NewGuid().ToString() + Input.UploadFile.FileName;
            category.PathOfImage = filePath;

            filePath = new Uri(Path.Join(_hostingEnvironment.WebRootPath, filePath)).LocalPath;
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await Input.UploadFile.CopyToAsync(fileStream);
            }

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}