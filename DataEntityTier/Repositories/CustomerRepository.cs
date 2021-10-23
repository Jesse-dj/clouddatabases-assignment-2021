using DataTier.Context;
using DataTier.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
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
        private readonly Container _container;
        public CustomerRepository(Container container)
        {
            _container = container;
        }

        public async Task<Customer> Add(Customer entity)
        {
            return await _container.CreateItemAsync(entity);
        }

        public async Task<List<Customer>> AllIncluding(params Expression<Func<Customer, object>>[] includeProperties)
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

        public async Task DeleteById(string id)
        {
            await _container.DeleteItemAsync<Customer>(id: id,  new PartitionKey(id));
        }

        public Task DeleteWhere(Expression<Func<Customer, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Customer>> FindBy(Expression<Func<Customer, bool>> predicate)
        {           
            return _container.GetItemLinqQueryable<Customer>(true)
                .Where(predicate).ToList();
        }

        public async Task<List<Customer>> GetAll()
        {
            return _container.GetItemLinqQueryable<Customer>(true).ToList();
        }

        public async Task<Customer> GetSingleById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Customer> GetSingle(Expression<Func<Customer, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<Customer> GetSingle(Expression<Func<Customer, bool>> predicate, params Expression<Func<Customer, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public async Task<Customer> Update(Customer entity)
        {
            return await _container.UpsertItemAsync(entity, new PartitionKey(entity.Id.ToString()));
        }
    }
}
