using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using QualitySouvenirs.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QualitySouvenirs.Models
{
    public class ShoppingCart
    {
        public string ID { get; set; }

        private const string CartSessionKey = "CardId";

        public static ShoppingCart GetShoppingCart(HttpContext context)
        {
            var cart = new ShoppingCart();
            cart.ID = cart.GetCartId(context);
            return cart;
        }

        public bool AddToCart(ApplicationContext db, int souvenirId, int count)
        {
            var souvenir = db.Souvenirs.SingleOrDefault(s => s.ID == souvenirId);
            if (souvenir != null)
            {
                var cartItem = db.CartItems.SingleOrDefault(c => c.CartID == ID && c.Souvenir.ID == souvenirId);
                if (cartItem == null)
                {
                    cartItem = new CartItem
                    {
                        CartID = ID,
                        Count = count,
                        Souvenir = souvenir,
                        DateCreated = DateTime.Now
                    };
                    db.CartItems.Add(cartItem);
                }
                else
                {
                    cartItem.Count += count;
                }
                db.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public void RemoveFromCart(ApplicationContext db, int souvenirId, int count)
        {
            var cartItem = db.CartItems.SingleOrDefault(c => c.CartID == ID && c.Souvenir.ID == souvenirId);
            if (cartItem != null)
            {
                cartItem.Count -= count;
                if (cartItem.Count <= 0)
                {
                    db.CartItems.Remove(cartItem);
                }
                db.SaveChanges();
            }
        }

        public void EmptyCart(ApplicationContext db)
        {
            var cartItems = db.CartItems.Where(c => c.CartID == ID);
            foreach (var cartItem in cartItems)
            {
                db.CartItems.Remove(cartItem);
            }
            db.SaveChanges();
        }

        public List<CartItem> GetCartItems(ApplicationContext db)
        {
            var cartItems = db.CartItems
                .Include(c => c.Souvenir)
                    .ThenInclude(s => s.Category)
                .Where(c => c.CartID == ID).ToList();
            return cartItems;
        }

        public string GetCartId(HttpContext context)
        {
            if (context.Session.GetString(CartSessionKey) == null)
            {
                Guid tempCartId = Guid.NewGuid();
                context.Session.SetString(CartSessionKey, tempCartId.ToString());
            }
            return context.Session.GetString(CartSessionKey);
        }
    }
}
