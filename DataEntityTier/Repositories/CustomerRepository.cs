using DataTier.Context;
using DataTier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataTier.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private CustomerContext context;

        public async Task<Customer> AddAsync(Customer entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Customer>> AllIncludingAsync(params Expression<Func<Customer, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Customer entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteWhereAsync(Expression<Func<Customer, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Customer>> FindBy(Expression<Func<Customer, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Customer> GetSingleAsync(uint id)
        {
            throw new NotImplementedException();
        }

        public async Task<Customer> GetSingleAsync(Expression<Func<Customer, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<Customer> GetSingleAsync(Expression<Func<Customer, bool>> predicate, params Expression<Func<Customer, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public async Task<Customer> UpdateAsync(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
