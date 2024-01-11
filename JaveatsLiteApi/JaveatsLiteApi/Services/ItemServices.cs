using JaveatsLiteApi.Data;
using JaveatsLiteApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JaveatsLiteApi.Services
{
    public class ItemServices : IItemServices
    {
        private readonly ApplicationDbContext _context;
        public ItemServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public Item Add(int menuID, Item item)
        {
            if (!MenuIsExistOrNot(menuID))
                return null;
            _context.Items.Add(item);
            _context.SaveChanges();
            return item;
        }

        public Item Delete(int itemID, int menuID)
        {
            if (!MenuIsExistOrNot(menuID)||!ItemIsExistOrNot(itemID))
                return null;
            var item = _context.Items.FirstOrDefault(e => e.ItemID == itemID);
            _context.Items.Remove(item);
            _context.SaveChanges();
            return item;
                
        }

        public Item Edit(Item newItem, int itemID)
        {
            if (!ItemIsExistOrNot(itemID))
                return null;
            _context.Items.Update(newItem);
            _context.SaveChanges();
            return newItem;
        }

        public List<Item> GetAll()
        {
            return _context.Items.ToList();
        }

        public Item GetById(int itemID)
        {
            return _context.Items.FirstOrDefault(e => e.ItemID == itemID);
        }
        public List<Item> GetAllByMenuID(int menuID)
        {
            return _context.Items.Where(e => e.MenuID == menuID).ToList();
        }
        public int GetAvailableQuantity(int itemID)
        {
            if (!ItemIsExistOrNot(itemID))
                return 0;
            return _context.Items.FirstOrDefault(e => e.ItemID == itemID).InStock;
        }
        private bool MenuIsExistOrNot(int menuId)
        {
            return _context.Menus.Any(e => e.ID == menuId);

        }
        private bool ItemIsExistOrNot(int itemId)
        {
            return _context.Items.Any(e => e.ItemID == itemId);

        }
    }
}
