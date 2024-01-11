using JaveatsLiteApi.Data;
using JaveatsLiteApi.DTO;
using JaveatsLiteApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JaveatsLiteApi.Services
{
    public interface IRestaurantServices
    {
        public List<Restaurant> GetAll();
        public Restaurant GetById(int RestaurantID);
        public Restaurant Add(Restaurant restaurant);
        public Restaurant Edit(Restaurant newRestaurant, int restaurantID);
        public Restaurant Delete(int RestaurantID);
    }
}
