using DataTier.Models;
using DataTier.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataTier.IServices
{
    public interface ICustomerService : IDbService<Customer>
    {
        Task<IEnumerable<Customer>> FindBy(Expression<Func<Customer, bool>> expression);
    }
}
