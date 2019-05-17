using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QualitySouvenirs.Data;
using QualitySouvenirs.Models;
using QualitySouvenirs.Share;

namespace QualitySouvenirs.Controllers
{
    [Authorize(Roles = "Customer")]
    public class OrdersController : Controller
    {
        private const double GST = 0.15;
        private readonly ApplicationContext _context;
        private readonly UserManager<AppUser> _userManager;

        public OrdersController(ApplicationContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Orders.ToListAsync());
        }

        public async Task<IActionResult> Create()
        {
            AppUser user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                ViewData["Address"] = user.Address;
                ViewData["PhoneNumber"] = user.PhoneNumber;
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Address,PhoneNumber")] Order order)
        {
            AppUser user = await _userManager.GetUserAsync(User);
            double subTotal = 0.0;
            double gst = 0.0;
            double grandTotal = 0.0;

            if (ModelState.IsValid)
            {
                var cart = ShoppingCart.GetShoppingCart(this.HttpContext);
                var cartItems = cart.GetCartItems(_context);
                var orderDetails = new List<OrderDetail>();
                foreach (var cartItem in cartItems)
                {
                    subTotal += cartItem.Souvenir.Price * cartItem.Count;
                    var orderDetail = new OrderDetail();
                    orderDetail.Quantity = cartItem.Count;
                    orderDetail.Souvenir = cartItem.Souvenir;
                    orderDetail.Order = order;
                    orderDetails.Add(orderDetail);
                    _context.Add(orderDetail);
                }
                gst += subTotal * GST;
                grandTotal += subTotal + gst;

                order.AppUser = user;
                order.SubTotal = subTotal;
                order.GST = gst;
                order.GrandTotal = grandTotal;
                order.OrderStatus = OrderStatus.Waiting;
                order.Date = DateTime.Today;
                order.OrderDetails = orderDetails;
                cart.ClearCart(_context);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }
    }
}
