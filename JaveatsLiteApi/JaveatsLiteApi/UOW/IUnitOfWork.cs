using JaveatsLiteApi.Interfaces;
using JaveatsLiteApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JaveatsLiteApi.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Restaurant> Restaurants{get;}
        IMenuRepository Menus { get; }
        IItemRepository Items { get; }
        int Complete();
    }
}
