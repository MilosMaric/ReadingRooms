using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IGenericRepository<T>
    {
        T Add(T entity);
        T Update(T entity);
        void Delete(long id);
        T GetById(long id);
        List<T> GetAll();
    }
}
