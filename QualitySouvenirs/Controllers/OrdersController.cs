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

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Orders.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
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
                //_context.Add(order);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName,Address,PhoneNumber,SubTotal,GrandTotal,OrderStatus,Date")] Order order)
        {
            if (id != order.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
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
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.ID == id);
        }
    }
}
