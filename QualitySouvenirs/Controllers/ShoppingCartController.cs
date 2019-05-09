using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QualitySouvenirs.Data;
using QualitySouvenirs.Models;

namespace QualitySouvenirs.Controllers
{
    [AllowAnonymous]
    public class ShoppingCartController : Controller
    {
        private const double GST = 0.15;
        private readonly ApplicationContext _context;

        public ShoppingCartController(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cart = ShoppingCart.GetShoppingCart(this.HttpContext);
            ViewData["GST"] = GST;

            return View(cart.GetCartItems(_context));
        }

        [HttpPost]
        public IActionResult Add(int? id, int? count)
        {
            if (id != null && count != null)
            {
                var cart = ShoppingCart.GetShoppingCart(this.HttpContext);
                if (cart.AddToCart(_context, id.Value, count.Value) == false)
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
            }
            
            return Ok();
        }

        [HttpPost]
        public IActionResult Remove(int? id, int? count)
        {
            if (id != null && count != null)
            {
                var cart = ShoppingCart.GetShoppingCart(this.HttpContext);
                cart.RemoveFromCart(_context, id.Value, count.Value);            
            }
            else
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}