using JaveatsLiteApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JaveatsLiteApi.Interfaces
{
    public interface IItemRepository:IBaseRepository<Item>
    {
        public int GetAvailableQuantity(int itemID);
        public List<Item> GetAllByMenuID(int menuID);
    }
}
