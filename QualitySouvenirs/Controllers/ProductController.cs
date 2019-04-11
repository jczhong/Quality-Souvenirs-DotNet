using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QualitySouvenirs.Data;
using QualitySouvenirs.Models;

namespace QualitySouvenirs.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly List<Category> categories;

        public ProductController(ApplicationContext context)
        {
            _context = context;
            categories = _context.Categories.ToList();
        }

        public async Task<IActionResult> Index(int? id)
        {
            ViewData["Categories"] = categories;

            if (id == null)
            {
                return View(await _context.Souvenirs.ToListAsync());
            }
            else
            {
                var souvenirs = await _context.Souvenirs
                            .Where(s => s.ID == id)
                            .ToListAsync();
                return View(souvenirs);
            }
        }
    }
}