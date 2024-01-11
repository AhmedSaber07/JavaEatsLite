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
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ItemController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("GetAllItems")]
        public IActionResult GetAll()
        {
            return Ok(_unitOfWork.Items.GetAll());
        }
        [HttpGet("GetItemsOfMenu")]
        public IActionResult GetAllItemsOfItem(int menuId)
        {
            return Ok(_unitOfWork.Items.GetAllByMenuID(menuId));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddItem(AddItemDto addItem)
        {
            if (ModelState.IsValid)
            {
                var item = new Item()
                {
                    Name = addItem.Name,
                    Description = addItem.Description,
                    ImageUrl = addItem.ImageUrl,
                    InStock = addItem.InStock,
                    IsPreferred = addItem.IsPreferred,
                    Price = addItem.Price,
                    MenuID = addItem.MenuID
                };
                var newItem = _unitOfWork.Items.Add(item);
                if (newItem is null)
                    return NotFound("Menu Not Found");
                return Ok(newItem);
            }
            return BadRequest(ModelState);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("ItemId:int")]
        public IActionResult EditItem(EditItemDto editItem, int ItemId)
        {
            if (ModelState.IsValid)
            {
                var Item = _unitOfWork.Items.GetById(ItemId);
                if (Item is null)
                    return NotFound("Item Not Found");
                var newItem = new Item();
                newItem.Name = Item.Name;
                newItem.Description = Item.Description;
                newItem.ImageUrl = Item.ImageUrl;
                newItem.InStock = Item.InStock;
                newItem.IsPreferred = Item.IsPreferred;
                newItem.Price = Item.Price;
                newItem.MenuID = Item.MenuID;
                return Ok(_unitOfWork.Items.Update(newItem));
            }
            return BadRequest(ModelState);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public IActionResult DeleteItem(int menuId, int ItemId)
        {
            var item = _unitOfWork.Items.GetById(ItemId);
            if (item is null)
                return NotFound("Item Not Found");
            _unitOfWork.Items.Delete(item);
            return Ok();
        }
    }
}
