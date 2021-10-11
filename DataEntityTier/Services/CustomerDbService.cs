using DataTier.Models;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataTier.Services
{
    public class CustomerDbService : IDbService<Customer>
    {
        private Container _container;

        public CustomerDbService(CosmosClient cosmosDbClient,string databaseName,string containerName)
        {
            _container = cosmosDbClient.GetContainer(databaseName, containerName);
        }

        public async Task<Customer> AddAsync(Customer customer)
        {
            var response = await _container.CreateItemAsync(customer, new PartitionKey(customer.id));
            return response.Resource;

        }

        public async Task DeleteAsync(string id)
        {
            await _container.DeleteItemAsync<Customer>(id: id, partitionKey: new PartitionKey(id));
        }

        public async Task<Customer> GetAsync(string id)
        {        
            try
            {
                var response = await _container.ReadItemAsync<Customer>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex)
            {
                Console.WriteLine("Cosmos Exception: " + ex.Message);
                return;
            }
        }

        public async Task<IEnumerable<Customer>> GetMultipleAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<Customer>(new QueryDefinition(queryString));

            var results = new List<Customer>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task<Customer> UpdateAsync(Customer customer)
        {
            var response = await _container.UpsertItemAsync(customer, new PartitionKey(customer.id));
            return response.Resource;
        }
    }
}
