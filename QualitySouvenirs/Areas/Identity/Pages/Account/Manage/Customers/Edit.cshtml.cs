using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QualitySouvenirs.Data;
using QualitySouvenirs.Models;

namespace QualitySouvenirs.Areas.Identity.Pages.Account.Manage.Customers
{
    public class EditModel : PageModel
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<AppUser> _userManager;

        public EditModel(ApplicationContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public class InputModel
        {
            public string ID { get; set; }

            public string Name { get; set; }

            public string Email { get; set; }

            [Required]
            [Display(Name = "Status: ")]
            public bool Status { get; set; }
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            Input = new InputModel
            {
                ID = user.Id,
                Name = user.FullName,
                Email = user.Email,
                Status = user.Enabled
            };

            ViewData["Status"] = new List<SelectListItem>
            {
                new SelectListItem { Value = Boolean.TrueString, Text = "Enable" },
                new SelectListItem { Value = Boolean.FalseString, Text = "Disable" },
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var id = Input.ID;

            var user = await _userManager.FindByIdAsync(id);
            user.Enabled = Input.Status;

            await _userManager.UpdateAsync(user);

            return RedirectToPage("./Index");
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.ID == id);
        }
    }
}
