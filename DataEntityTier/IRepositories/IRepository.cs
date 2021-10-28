using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataTier.Repositories
{
    public interface IRepository<T, ID> where T : class, new()
    {
        int Count();
        Task<List<T>> GetAll();
        Task<T> FindById(ID id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task DeleteById(ID id);
    }
}
