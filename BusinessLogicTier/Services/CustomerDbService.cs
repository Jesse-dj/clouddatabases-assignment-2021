using DataTier.IServices;
using DataTier.Models;
using DataTier.Repositories;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataTier.Services
{
    public class CustomerDbService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerDbService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Customer> Add(Customer customer)
        {
            try
            {
                return await _repository.Add(customer);
            }
            catch (CosmosException ex)
            {
                Console.WriteLine("Cosmos Exception: " + ex);
                return null;
            }
        }

        public async Task DeleteById(string id)
        {
            try
            {
                await _repository.DeleteById(id);
            }
            catch (CosmosException)
            {
                return;
            }
        }

        public async Task<List<Customer>> FindBy(params Expression<Func<Customer,bool>>[] predicates)
        {
            try
            {
                return await _repository.AllIncluding(predicates);
            }
            catch (CosmosException)
            {

                throw;
            }
        }

        public async Task<Customer> GetById(string id)
        {        
            try
            {
                return await _repository.FindById(id);
            }
            catch (CosmosException)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Customer>> GetMultiple(string queryString)
        {
            try
            {
                return await _repository.GetAll();
            }
            catch (CosmosException)
            {

                return null;
            }
        }

        public async Task<Customer> Update(Customer customer)
        {
            try
            {
                return await _repository.Update(customer);
            }
            catch (CosmosException)
            {
                return null;
            }           
        }
    }
}
