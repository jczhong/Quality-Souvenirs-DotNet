using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QualitySouvenirs.Data;
using QualitySouvenirs.Models;
using QualitySouvenirs.Share;

namespace QualitySouvenirs.Areas.Identity.Pages.Account.Manage
{
    public class OrdersModel : PageModel
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<AppUser> _userManager;

        public OrdersModel(ApplicationContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Order> Order { get;set; }

        public async Task OnGetAsync()
        {
            AppUser user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                if (await _userManager.IsInRoleAsync(user, Roles.Admin))
                {
                    Order = await _context.Orders.ToListAsync();
                }
                else if (await _userManager.IsInRoleAsync(user, Roles.Customer))
                {
                    Order = await _context.Orders.Where(o => o.AppUser == user).ToListAsync();
                }
            }
        }
    }
}
