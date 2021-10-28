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
        Task<List<Customer>> FindBy(params Expression<Func<Customer, bool>>[] predicates);
    }
}
