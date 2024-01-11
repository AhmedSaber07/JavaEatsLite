using JaveatsLiteApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JaveatsLiteApi.Interfaces
{
    public interface IMenuRepository:IBaseRepository<Menu>
    {
        public List<Menu> GetAllByRestaurantID(int restaurantId);
    }
}
