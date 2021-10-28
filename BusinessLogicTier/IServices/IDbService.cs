using DataTier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTier.Services
{
    public interface IDbService<T>
    {
        Task<IEnumerable<T>> GetMultiple(string query);
        Task<T> GetById(string id);
        Task<T> Add(T item);
        Task<T> Update(T item);
        Task DeleteById(string id);
    }
}
