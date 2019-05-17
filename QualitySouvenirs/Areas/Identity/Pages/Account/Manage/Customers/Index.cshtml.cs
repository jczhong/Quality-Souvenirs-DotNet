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

namespace QualitySouvenirs.Areas.Identity.Pages.Account.Manage.Customers
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<AppUser> _userManager;

        public IndexModel(ApplicationContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IEnumerable<AppUser> AppUsers { get;set; }

        public async Task OnGetAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            AppUsers = _userManager.Users.Where(u => u.Id != currentUser.Id);
        }
    }
}
