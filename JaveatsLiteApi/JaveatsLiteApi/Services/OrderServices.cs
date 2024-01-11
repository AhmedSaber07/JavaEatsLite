using JaveatsLiteApi.Data;
using JaveatsLiteApi.DTO;
using JaveatsLiteApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JaveatsLiteApi.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly ApplicationDbContext _context;
        private readonly ShoppingCart _shoppingCart;
        public OrderServices(ApplicationDbContext context, ShoppingCart shoppingCart)
        {
            _context = context;
            _shoppingCart = shoppingCart;
        }
        public Order AddOrder(Order order)
        {
            if (!(_context.Restaurants.Any(e => e.ID == order.RestaurantID)) || !(_context.Users.Any(e => e.Id == order.UserID)))
                return null;
            //var checkOrder = _context.Orders.FirstOrDefault(e => e.UserID == order.UserID && (e.Status=="Pending" || string.IsNullOrEmpty(e.Status)) && e.RestaurantID == order.RestaurantID);
            //if (checkOrder is null)
            //{
            //}
            //else
            //{
            //var neworderdetails = _context.OrderDetails.Where(e => e.OrderId == order.ID);
            //_context.OrderDetails.RemoveRange(neworderdetails);
            //_context.Orders.Remove(_context.Orders.FirstOrDefault(e => e.RestaurantID == order.RestaurantID));
            //_context.SaveChanges();
            //neworder.id = 0;
            //neworder.orderdetails = null;
            //checkOrder.OrderTotal = _shoppingCart.CalcTotal(checkOrder.RestaurantID);
            //    checkOrder.Updated_at = DateTime.UtcNow;
            //    _context.Orders.Update(checkOrder);
            //    _context.SaveChanges();
            //    _context.OrderDetails.RemoveRange(_context.OrderDetails.Where(e=>e.OrderId==checkOrder.ID));
            //    _context.SaveChanges();
            //}
            order.Created_at = DateTime.UtcNow;
            order.OrderTotal = _shoppingCart.CalcTotal(order.RestaurantID);
            order.Status = "Pending";
            _context.Orders.Add(order);
            _context.SaveChanges();
            var CartItems = _shoppingCart.ShoppingCartItems.Where(e => e.RestaurantId == order.RestaurantID);
            foreach (var cartItem in CartItems)
            {   
                var orderDetails = new OrderDetails()
                {
                    Quantity = cartItem.Quantity,
                    Price = cartItem.Item.Price,
                    OrderTotal = cartItem.TotalPrice,
                    ItemId = cartItem.Item.ItemID,
                    OrderId = order.ID,
                    MenuId = _context.Items.FirstOrDefault(e=>e.ItemID== cartItem.Item.ItemID).MenuID
                };
                _context.OrderDetails.Add(orderDetails);
            }
            _context.SaveChanges();
            _shoppingCart.ClearCart();
            return order;
        }

        //public Order DeleteOrder(int orderID, int restaurantID, string userID)
        //{
        //    if (!isExistOrNot(orderID)||!(_context.Restaurants.Any(e => e.ID == restaurantID))||!(_context.Users.Any(e => e.Id == userID)))
        //        return null;
        //    var order = GetOrder(userID, restaurantID, orderID);
        //    _context.Orders.Remove(order);
        //    _context.SaveChanges();
        //    return order;
        //}

        public IEnumerable<Order> GetAllOrders()
        {
            return _context.Orders.Include(e=>e.OrderDetails).ThenInclude(t=>t.Item).ToList();
        }

        public Order GetOrder(string userID, int restaurantID, int orderID)
        {
            return _context.Orders.Include(e=>e.OrderDetails).ThenInclude(t => t.Item).FirstOrDefault(e =>e.ID==orderID && e.UserID == userID && restaurantID == e.RestaurantID);
        }

        public IEnumerable<Order> GetRestaurantOrders(int restaurantID)
        {
            return _context.Orders.Include(e=>e.OrderDetails).ThenInclude(t => t.Item).Where(e => e.RestaurantID == restaurantID).ToList();
        }

        public IEnumerable<Order> GetUserOrders(string userID)
        {
            return _context.Orders.Include(e => e.OrderDetails).ThenInclude(t => t.Item).Where(e => e.UserID == userID).ToList();
        }

        public IEnumerable<Order> GetUserOrdersInRestaurant(string userID, int restaurantID)
        {
            return _context.Orders.Include(e => e.OrderDetails).ThenInclude(t => t.Item).Where(e => e.UserID == userID && e.RestaurantID == restaurantID).ToList();
        }

        public bool ConfirmOrder(int orderID)
        {
            if(isExistOrNot(orderID))
            {
                var order = _context.Orders.FirstOrDefault(e => e.ID == orderID);
                if (DateTime.UtcNow.Minute-(order.Created_at>order.Updated_at?order.Created_at.Minute:order.Updated_at.Minute)>=3)
                {
                    order.Status = "Confirmed";
                    _shoppingCart.ClearCart();
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            return false;
        }
        public bool CancelOrder(int orderID)
        {
            if(isExistOrNot(orderID))
            {
                var order = _context.Orders.Include(e=>e.OrderDetails).FirstOrDefault(e => e.ID == orderID);
                if (order.Status == "Confirmed")
                    return false;
                else
                {
                    order.Status = "Cancelled";
                    foreach (var orderDetails in order.OrderDetails)
                    {

                        _shoppingCart.RemoveFromCart(_context.Items.FirstOrDefault(e => e.ItemID == orderDetails.ItemId),order.RestaurantID, Convert.ToInt32(orderDetails.Quantity));
                        _shoppingCart.UpdateItemQuantityWhenRemove(orderDetails.ItemId, Convert.ToInt32(orderDetails.Quantity));
                    }
                    _context.Orders.Remove(order);
                    _context.SaveChanges();
                    return true;
                }    
            }
            return false;
        }

        //public Order UpdateOrder(int orderID, int restaurantID, string userID, Order newOrder)
        //{
        //    if (!isExistOrNot(orderID) || !(_context.Restaurants.Any(e => e.ID == restaurantID)) || !(_context.Users.Any(e => e.Id == userID)))
        //        return null;
        //    _context.Orders.Update(newOrder);
        //    _context.SaveChanges();
        //    return newOrder;
        //}
        private bool isExistOrNot(int orderId)
        {
            return _context.Orders.Any(e => e.ID == orderId);
        }
    }
}
