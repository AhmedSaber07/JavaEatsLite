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
    public class RestaurantServices : IRestaurantServices
    {
        private readonly ApplicationDbContext _context;
        public RestaurantServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public Restaurant Add(Restaurant restaurant)
        {
            _context.Restaurants.Add(restaurant);
            _context.SaveChanges();
            return restaurant;
        }

        public Restaurant Delete(int RestaurantID)
        {
            var restaurant = GetById(RestaurantID);
            if (restaurant is null)
                return null;
            _context.Restaurants.Remove(restaurant);
            _context.SaveChanges();
            return restaurant;
        }

        public Restaurant Edit(Restaurant newRestaurant, int restaurantID)
        {

            /*                _context.Entry(restaurant).CurrentValues.SetValues(newRestaurant)*/
            ;
            _context.Restaurants.Update(newRestaurant);
            _context.SaveChanges();
            return newRestaurant;

        }

        public List<Restaurant> GetAll()
        {
            return _context.Restaurants.ToList();
        }

        public Restaurant GetById(int RestaurantID)
        {
            if(isExistOrNot(RestaurantID))
            return _context.Restaurants.Include(e=>e.Menus).FirstOrDefault(e => e.ID == RestaurantID);
            return null;
        }
        private bool isExistOrNot(int restaurantid)
        {
            return _context.Restaurants.Any(e => e.ID == restaurantid);
        }

    }
}
