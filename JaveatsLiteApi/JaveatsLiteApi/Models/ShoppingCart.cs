using JaveatsLiteApi.Data;
using JaveatsLiteApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JaveatsLiteApi.Models
{
    public class ShoppingCart
    {
        private readonly ApplicationDbContext _context;
        
        public ShoppingCart(ApplicationDbContext context)
        {
            _context = context;
        }

        public string ShoppingCartId { get; set; }
        public List<CartItem> ShoppingCartItems { get; set; }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<ApplicationDbContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);
            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }
        public void AddToCart(Item item,int quantity)
        {
            var cartItem = _context.ShoppingCartItems.FirstOrDefault(e => e.Item.ItemID== item.ItemID && e.ShoppingCartId == ShoppingCartId);
            if(cartItem==null)
            {
                if (UpdateItemQuantityWhenAdd(item.ItemID,1))

                {
                    cartItem = new CartItem()
                    {
                        ShoppingCartId = ShoppingCartId,
                        RestaurantId = _context.Menus.FirstOrDefault(e=>e.ID==item.MenuID).restaurantID,
                        Item = item,
                        Price = item.Price,
                        TotalPrice = item.Price * quantity,
                        Created_at = DateTime.UtcNow,
                        Quantity = 1
                    };
                    _context.ShoppingCartItems.Add(cartItem);
                }
            }
            else
            {
                if (UpdateItemQuantityWhenAdd(item.ItemID, quantity))
                {
                    cartItem.Quantity += quantity;
                    cartItem.Updated_at = DateTime.UtcNow;
                    cartItem.TotalPrice += (quantity * item.Price);
                    //_context.SaveChanges();
                }
            }
            _context.SaveChanges();
        }
        public bool RemoveFromCart(Item item,int restaurantId,int quantity)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(e => e.Item.ItemID == item.ItemID && e.RestaurantId == restaurantId && e.ShoppingCartId == ShoppingCartId);
            if (shoppingCartItem != null)
            {
                if (quantity <= shoppingCartItem.Quantity)
                {
                    if (quantity < shoppingCartItem.Quantity)
                    {
                        shoppingCartItem.Quantity -= quantity;
                        UpdateItemQuantityWhenRemove(item.ItemID, quantity);
                        
                            shoppingCartItem.TotalPrice -= (quantity * item.Price);
                            //_context.SaveChanges();
                        
                    }
                    else
                    {
                        _context.ShoppingCartItems.Remove(shoppingCartItem);
                        UpdateItemQuantityWhenRemove(item.ItemID, quantity);
                    }
                    shoppingCartItem.Updated_at = DateTime.UtcNow;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
                _context.SaveChanges();
            return true;
        }
        public List<CartItem> GetCartItems()
        {
            return ShoppingCartItems??
                (ShoppingCartItems = 
                _context.ShoppingCartItems.Where(e => e.ShoppingCartId == ShoppingCartId)
                .Include(e => e.Item)
                .ToList());
        }

        public decimal CalcTotal(int? restaurantId)
        {
            var total = _context.ShoppingCartItems.Where(e => e.ShoppingCartId == ShoppingCartId && e.RestaurantId == restaurantId).Select(e => e.Quantity * e.Item.Price).Sum();
            return total;
        }

        public void ClearCart()
        {
            var cartItems = _context.ShoppingCartItems.Include(e=>e.Item).Where(e => e.ShoppingCartId == ShoppingCartId);
            _context.ShoppingCartItems.RemoveRange(cartItems);
            _context.SaveChanges();
        }
        public bool UpdateItemQuantityWhenAdd(int itemID,int quantity)
        {
            var getItem = _context.Items.FirstOrDefault(e => e.ItemID == itemID);
            if (getItem.InStock > 0)
            {
                getItem.InStock -= quantity;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public void UpdateItemQuantityWhenRemove(int itemID, int quantity)
        {
            var getItem = _context.Items.FirstOrDefault(e => e.ItemID == itemID);
                getItem.InStock += quantity;
                _context.SaveChanges();
        }


    }
}
