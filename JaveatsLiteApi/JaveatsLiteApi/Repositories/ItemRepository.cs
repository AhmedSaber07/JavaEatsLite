using JaveatsLiteApi.Data;
using JaveatsLiteApi.Interfaces;
using JaveatsLiteApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JaveatsLiteApi.Repositories
{
    public class ItemRepository : BaseRepository<Item>, IItemRepository
    {
        private readonly ApplicationDbContext _context;
        public ItemRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
        public List<Item> GetAllByMenuID(int menuID)
        {
            return _context.Items.Where(e => e.MenuID == menuID).ToList();
        }
        public int GetAvailableQuantity(int itemID)
        {
            return _context.Items.FirstOrDefault(e => e.ItemID == itemID).InStock;
        }
    }
}
