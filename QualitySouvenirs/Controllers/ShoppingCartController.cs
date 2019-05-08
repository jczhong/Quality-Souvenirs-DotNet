using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QualitySouvenirs.Data;

namespace QualitySouvenirs.Controllers
{
    [AllowAnonymous]
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationContext _context;

        public ShoppingCartController(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(int? id, int? quantity)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Remove(int? id, int? quantity)
        {
            return Ok();
        }
    }
}