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
    public class RestaurantController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public RestaurantController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_unitOfWork.Restaurants.GetAll());
        }
        [HttpGet("id:int")]
        public IActionResult Details(int id)
        {
            var restaurant = _unitOfWork.Restaurants.GetById(id);
            if (restaurant is null)
                return NotFound("This Restaurant Not Found");
            return Ok(restaurant);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddRestaurant(AddRestaurantDto restaurantDto)
        {
            if (ModelState.IsValid)
            {
                var restaurant = new Restaurant()
                {
                    Description = restaurantDto.Description,
                    Location = restaurantDto.Location,
                    Created_at = DateTime.UtcNow
                };
                return Ok(_unitOfWork.Restaurants.Add(restaurant));
            }
            return BadRequest(ModelState);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("restaurantID:int")]
        public IActionResult EditRestaurant(int restaurantID,EditRestaurantDto restaurantDto)
        {
            if (ModelState.IsValid)
            {
                var newRestaurant = _unitOfWork.Restaurants.GetById(restaurantID);
                if (newRestaurant is null)
                    return NotFound("This Restaurant Not Found");
                newRestaurant.Description = restaurantDto.Description;
                newRestaurant.Location = restaurantDto.Location;
                newRestaurant.Updated_at = DateTime.UtcNow;
               return Ok(_unitOfWork.Restaurants.Update(newRestaurant));               
            }
            return BadRequest(ModelState);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public IActionResult DeleteRestaurnt(int restaurantID)
        {
            if(ModelState.IsValid)
            {
                var restaurant = _unitOfWork.Restaurants.GetById(restaurantID);
                if (restaurant is null)
                    return NotFound("This Restaurant Not Found");
                _unitOfWork.Restaurants.Delete(restaurant);
                return Ok(restaurant);
            }
            return BadRequest(ModelState);
        }
    }
}
