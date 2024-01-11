using JaveatsLiteApi.DTO;
using JaveatsLiteApi.Models;
using JaveatsLiteApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JaveatsLiteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _orderServices;
        private readonly ShoppingCart _shoppingCart;
        public OrderController(IOrderServices orderServices, ShoppingCart shoppingCart)
        {
            _orderServices = orderServices;
            _shoppingCart = shoppingCart;
        }
        [HttpGet("GetAllOrders")]
        public IActionResult GetAllOrders()
        {
            return Ok(_orderServices.GetAllOrders());
        }
        [HttpGet("GetRestaurantOrders")]
        public IActionResult GetRestaurantOrders(int restaurantID)
        {
            return Ok(_orderServices.GetRestaurantOrders(restaurantID));
        }
        [HttpGet("GetUserOrders")]
        public IActionResult GetUserOrders(string userID)
        {
            return Ok(_orderServices.GetUserOrders(userID));
        }
        [HttpGet("GetUserOrdersInRestaurant")]
        public IActionResult GetUserOrdersInRestaurant(string userID, int restaurantID)
        {
            return Ok(_orderServices.GetUserOrdersInRestaurant(userID, restaurantID));
        }
        [HttpGet("GetOrder")]
        public IActionResult GetOrder(string userID, int restaurantID, int orderID)
        {
            return Ok(_orderServices.GetOrder(userID,restaurantID,orderID));
        }
        //[HttpPost]
        //public IActionResult AddOrder(AddOrderDto addOrder)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        var order = new Order()
        //        {
        //            RestaurantID = addOrder.RestaurantID,
        //            UserID = addOrder.UserID,
        //            Status = addOrder.Status,
        //            OrderTotal = addOrder.OrderTotal
        //        };
        //        var result = _orderServices.AddOrder(order);
        //        if (result is null)
        //            return NotFound();
        //       return Ok(result);
        //    }
        //    return BadRequest(ModelState);
        //}
        [HttpPost("CheckOut")]
        public IActionResult CheckOut(AddOrderDto addOrder)
        {
            _shoppingCart.ShoppingCartItems = _shoppingCart.GetCartItems();
            if(_shoppingCart.ShoppingCartItems.Count==0)
            {
                return BadRequest("Your Card is empty, Add Some Items First");
            }
            if(ModelState.IsValid)
            {
                var order = new Order()
                {
                    RestaurantID = addOrder.RestaurantID,
                    UserID = addOrder.UserID
                };
                var result = _orderServices.AddOrder(order);
                if (result is null)
                    return NotFound();
              //  _shoppingCart.ClearCart();
                return Ok("Order Added Successfully");
            }
            return BadRequest(ModelState);
        }
        [HttpPost("Cancel Order")]
        public IActionResult CancelOrder(int orderID)
        {
            if (_orderServices.ConfirmOrder(orderID))
                return Ok("Order is Confirmed");
            else
            {
                _orderServices.CancelOrder(orderID);
                return Ok("Order is Cancelled");
            }

        }
        //[Authorize(Roles = "Admin")]
        //[HttpPut("orderID:int,userID:string,restaurantID:int")]
        //public IActionResult EditOrder(int orderID,string userID,int restaurantID,EditOrderDto editOrder)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        var orderAfterEdited = _orderServices.GetOrder(userID, restaurantID, orderID);
        //        if (orderAfterEdited is null)
        //            return NotFound("Order Not Found");
        //        orderAfterEdited.ID = orderID;
        //        orderAfterEdited.RestaurantID = restaurantID;
        //        orderAfterEdited.UserID = userID;
        //        orderAfterEdited.Updated_at = DateTime.UtcNow;
        //        orderAfterEdited.Status = editOrder.Status;
        //      return Ok(_orderServices.UpdateOrder(orderID, restaurantID, userID, orderAfterEdited));
                
        //    }
        //    return BadRequest(ModelState);
        //}
        //[Authorize(Roles = "Admin")]
        //[HttpDelete("orderID:int,userID:string,restaurantID:int")]
        //public IActionResult DeleteOrder(int orderID, int restaurantID, string userID)
        //{
        //    var result = _orderServices.DeleteOrder(orderID, restaurantID, userID);
        //    if (result is null)
        //        return NotFound("Order Not Found");
        //    return Ok(result);
        //}
    }
}
