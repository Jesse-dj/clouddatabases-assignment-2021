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
        Task<List<T>> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        Task<List<T>> GetAll();
        int Count();
        Task<T> GetSingleById(ID id);
        Task<T> GetSingle(Expression<Func<T, bool>> predicate);
        Task<T> GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<List<T>> FindBy(Expression<Func<T, bool>> predicate);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task DeleteById(ID id);
        Task DeleteWhere(Expression<Func<T, bool>> predicate);
        void Commit();
    }
}
