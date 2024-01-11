using JaveatsLiteApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JaveatsLiteApi.Services
{
    public interface IItemServices
    {
        public List<Item> GetAll();
        public List<Item> GetAllByMenuID(int menuID);
        public Item GetById(int itemID);
        public int GetAvailableQuantity(int itemID);
        public Item Add(int menuID, Item item);
        public Item Edit(Item newItem, int itemID);
        public Item Delete(int itemID, int menuID);
    }
}
