using JaveatsLiteApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JaveatsLiteApi.Services
{
    public interface IOrderServices
    {
        IEnumerable<Order> GetAllOrders();
        IEnumerable<Order> GetRestaurantOrders(int restaurantID);
        IEnumerable<Order> GetUserOrders(string userID);

        IEnumerable<Order> GetUserOrdersInRestaurant(string userID, int restaurantID);
        Order GetOrder(string userID, int restaurantID, int orderID);
        Order AddOrder(Order order);
        bool ConfirmOrder(int orderID);
        bool CancelOrder(int orderID);

        //Order UpdateOrder(int orderID, int restaurantID, string userID,Order newOrder);
        //Order DeleteOrder(int orderID, int restaurantID, string userID);
    }
}
