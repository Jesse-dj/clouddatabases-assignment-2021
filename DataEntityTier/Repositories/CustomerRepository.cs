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

        public async Task<List<Customer>> AllIncluding(params Expression<Func<Customer, bool>>[] includeProperties)
        {
            IEnumerable<Customer> queryable = _container.GetItemLinqQueryable<Customer>();
            foreach (var includeProperty in includeProperties)
            {
                queryable = queryable.Where(includeProperty.Compile());
            }
            return queryable.ToList();
        }

        public int Count()
        {
            return _container.GetItemLinqQueryable<Customer>().Count();
        }

        public async Task DeleteById(string id)
        {
            await _container.DeleteItemAsync<Customer>(id: id,  new PartitionKey(id));
        }

        public async Task<List<Customer>> GetAll()
        {
            return _container.GetItemLinqQueryable<Customer>(true).ToList();
        }

        public async Task<Customer> FindById(string id)
        {
            return await _container.ReadItemAsync<Customer>(id: id, new PartitionKey(id));
        }

        public async Task<Customer> Update(Customer entity)
        {
            return await _container.UpsertItemAsync(entity, new PartitionKey(entity.Id.ToString()));
        }
    }
}
