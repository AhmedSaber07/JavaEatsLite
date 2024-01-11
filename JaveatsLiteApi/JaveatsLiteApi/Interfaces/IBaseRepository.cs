using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JaveatsLiteApi.Interfaces
{
    public interface IBaseRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        T Add(T entity);
        T Update(T entity);
        void Delete(T entity);
    }
}
