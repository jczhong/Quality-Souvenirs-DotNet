using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QualitySouvenirs.Data;
using QualitySouvenirs.Models;

namespace QualitySouvenirs.Areas.Identity.Pages.Account.Manage.Orders
{
    [Authorize(Roles = "Administrator")]
    public class EditModel : PageModel
    {
        private readonly ApplicationContext _context;

        public EditModel(ApplicationContext context)
        {
            _context = context;
        }

        public OrderStatusViewModel OrderStatus { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public int ID { get; set; }

            [Display(Name = "Name")]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "Status: ")]
            public string Status { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FirstOrDefaultAsync(m => m.ID == id);

            if (order == null)
            {
                return NotFound();
            }

            Input = new InputModel
            {
                ID = order.ID,
                LastName = order.LastName,
                Status = order.OrderStatus
            };

            OrderStatus = new OrderStatusViewModel();
            OrderStatus.Status = order.OrderStatus;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid && id == null)
            {
                return Page();
            }

            var order = await _context.Orders.FirstOrDefaultAsync(m => m.ID == id.Value);
            order.OrderStatus = Input.Status;
            _context.Attach(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(order.ID))
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

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.ID == id);
        }
    }
}
