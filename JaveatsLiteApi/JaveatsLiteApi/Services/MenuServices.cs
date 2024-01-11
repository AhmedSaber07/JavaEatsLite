using JaveatsLiteApi.Data;
using JaveatsLiteApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JaveatsLiteApi.Services
{
    public class MenuServices : IMenuServices
    {
        private readonly ApplicationDbContext _context;
        public MenuServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public Menu Add(int restaurantId,Menu menu)
        {
            var checkRestaurant = _context.Restaurants.Any(e => e.ID == restaurantId);
            if (!checkRestaurant)
                return null;
           _context.Menus.Add(menu);
            _context.SaveChanges();
            return menu;
        }

        public Menu Delete(int restaurantId,int menuID)
        {
            var checkMenu = isExistOrNot(menuID);
            if (!checkMenu)
                return null;
            var menu = _context.Menus.FirstOrDefault(e => e.ID == menuID && e.restaurantID==restaurantId);
            _context.Menus.Remove(menu);
            _context.SaveChanges();
            return menu;
        }

        public Menu Edit(Menu newMenu, int menuID)
        {
            var checkMenu = isExistOrNot(menuID);
            if (!checkMenu)
                return null;
            _context.Menus.Update(newMenu);
            _context.SaveChanges();
            return newMenu;
        }

        public List<Menu> GetAllByRestaurantID(int restaurantId)
        {
            return _context.Menus.Where(e => e.restaurantID == restaurantId).ToList();
        }

        public Menu GetById(int restaurantId, int menuId)
        {
            return _context.Menus.Include(e=>e.Items).FirstOrDefault(e => e.restaurantID == restaurantId && e.ID == menuId);
        }
        private bool isExistOrNot(int menuId)
        {
            return _context.Menus.Any(e => e.ID == menuId);
            
        }
    }
}
