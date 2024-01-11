using JaveatsLiteApi.DTO;
using JaveatsLiteApi.Models;
using JaveatsLiteApi.Services;
using JaveatsLiteApi.UOW;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JaveatsLiteApi.Controllers
{
   [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly ShoppingCart _shoppingCart;
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartController(ShoppingCart shoppingCart, IUnitOfWork unitOfWork)
        {
            _shoppingCart = shoppingCart;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult GetShoppingCartItems()
        {
            var shoppingCartDto = new ShoppingCartDto()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.CalcTotal(null)
            };
            return Ok(shoppingCartDto);
        }
        [HttpPost]
        public IActionResult AddToShoppingCart(AddToShoppingCartDto body)
        {
            var selectedItem = _unitOfWork.Items.GetById(body.itemId);
            if(selectedItem!=null)
            {
                if(body.quantity==0)
                _shoppingCart.AddToCart(selectedItem, 1);
                else
                    _shoppingCart.AddToCart(selectedItem, body.quantity);
                return Ok("Item Added To Cart Successfully");
            }
            return NotFound("Item Not Found");
        }
        [HttpDelete]
        public IActionResult RemoveFromShoppingCart(RemoveFromShoppingCartDto body)
        {
            var selectedItem = _unitOfWork.Items.GetById(body.itemId);
            if (selectedItem != null)
            {
                if (body.quantity >= 0 || body.quantity == null)
                {
                    bool checkQuantity = _shoppingCart.RemoveFromCart(selectedItem, body.restaurantId, (body.quantity ?? 1));

                    if (checkQuantity)
                        return Ok("Item Deleted From Cart Successfully");
                    else
                        return NotFound("This Item is Not Found in This Cart");
                }
                return BadRequest("Please Enter Valid Quantity");
            }
            return NotFound("This Item is Not Found in This Menu");
        }
    }
}
