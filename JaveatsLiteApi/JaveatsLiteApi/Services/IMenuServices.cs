using JaveatsLiteApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JaveatsLiteApi.Services
{
    public interface IMenuServices
    {
        public List<Menu> GetAllByRestaurantID(int restaurantId);
        public Menu GetById(int restaurantId,int menuId);
        public Menu Add(int restaurantId,Menu menu);
        public Menu Edit(Menu newMenu, int menuID);
        public Menu Delete(int restaurantId,int menuID);
    }
}
