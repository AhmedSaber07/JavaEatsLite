using JaveatsLiteApi.Data;
using JaveatsLiteApi.Interfaces;
using JaveatsLiteApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JaveatsLiteApi.Repositories
{
    public class MenuRepository : BaseRepository<Menu>,IMenuRepository
    {
        ApplicationDbContext _context;
        public MenuRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
        public List<Menu> GetAllByRestaurantID(int restaurantId)
        {
            return _context.Menus.Where(e => e.restaurantID == restaurantId).ToList();
        }
    }
}
