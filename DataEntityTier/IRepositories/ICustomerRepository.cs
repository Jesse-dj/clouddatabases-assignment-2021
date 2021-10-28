using DataTier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataTier.Repositories
{
    public interface ICustomerRepository : IRepository<Customer, string>
    {
        Task<List<Customer>> AllIncluding(params Expression<Func<Customer, bool>>[] includeProperties);
    }
}
