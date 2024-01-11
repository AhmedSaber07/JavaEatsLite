using JaveatsLiteApi.Data;
using JaveatsLiteApi.Interfaces;
using JaveatsLiteApi.Models;
using JaveatsLiteApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JaveatsLiteApi.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IBaseRepository<Restaurant> Restaurants { get; private set; }

        public IMenuRepository Menus { get; private set; }

        public IItemRepository Items { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Restaurants = new BaseRepository<Restaurant>(_context);
            Menus = new MenuRepository(_context);
            Items = new ItemRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
