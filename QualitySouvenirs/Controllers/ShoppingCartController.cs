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

            double subTotal = 0.0;
            double gst = 0.0;
            double grandTotal = 0.0;
        
            var cartItems = cart.GetCartItems(_context);
            foreach (var cartItem in cartItems)
            {
                subTotal += cartItem.Souvenir.Price * cartItem.Count;
            }
            gst += subTotal * GST;
            grandTotal += subTotal + gst;

            ViewData["SubTotal"] = subTotal;
            ViewData["GST"] = gst;
            ViewData["GrandTotal"] = grandTotal;

            return View(cart.GetCartItems(_context));
        }

        [HttpPost]
        public IActionResult AddPOST(int? id, int? count)
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

        public IActionResult AddGET(int? id, int? count)
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

            return RedirectToAction("Index");
        }

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
            return RedirectToAction("Index");
        }

        public IActionResult ClearCart()
        {
            var cart = ShoppingCart.GetShoppingCart(this.HttpContext);
            cart.ClearCart(_context);

            return RedirectToAction("Index");
        }

        public ContentResult GetCount()
        {
            var cart = ShoppingCart.GetShoppingCart(this.HttpContext);
            var count = cart.GetCount(_context);
 
            return Content(count.ToString());
        }
    }
}