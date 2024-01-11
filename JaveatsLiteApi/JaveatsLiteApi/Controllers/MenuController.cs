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
    public class MenuController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public MenuController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("GetAllMenusOfRestaurant")]
        public IActionResult GetAllMenusOfRestaurant(int restaurantId)
        {
            return Ok(_unitOfWork.Menus.GetAllByRestaurantID(restaurantId));
        }
        [HttpGet("GetSpecificMenuOfRestaurant")]
        public IActionResult GetMenuOfRestaurant(int menuId)
        {
            return Ok(_unitOfWork.Menus.GetById(menuId));
        }
        [Authorize(Roles ="Admin")]
        [HttpPost("Create Menu")]
        public IActionResult AddMenu(AddMenuDto addMenu)
        {
            if(ModelState.IsValid)
            {
                var menu = new Menu()
                {
                    restaurantID = addMenu.restaurantID,
                    Name = addMenu.Name,
                    Created_at = DateTime.UtcNow
                };
                var newMenu = _unitOfWork.Menus.Add(menu);
                if (newMenu is null)
                    return NotFound("Restaurant Not Found");
                return Ok(newMenu);
            }
            return BadRequest(ModelState);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("Edit Menu/{menuId:int}")]
        public IActionResult EditMenu(EditMenuDto editMenu,int menuId)
        {
            if(ModelState.IsValid)
            {
                var menu = _unitOfWork.Menus.GetById(menuId);
                if (menu is null)
                    return NotFound("Menu Not Found");
                var newMenu = new Menu();
                newMenu.restaurantID = editMenu.restaurantID;
                newMenu.ID = menuId;
                newMenu.Name = editMenu.Name;
                newMenu.Updated_at = DateTime.UtcNow;
               return Ok(_unitOfWork.Menus.Update(newMenu));
            }
            return BadRequest(ModelState);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("Remove Menu")]
        public IActionResult DeleteMenu(int restaurantId,int menuId)
        {
            var menu = _unitOfWork.Menus.GetById(menuId);
            if(menu is null)
                return NotFound("Menu Not Found");
            _unitOfWork.Menus.Delete(menu);
            return Ok("Menu Deleted");
        }
    }
}
