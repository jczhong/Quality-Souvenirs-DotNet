using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index(int? id)
        {

            ViewData["Categories"] = categories;
            return View();
        }
    }
}