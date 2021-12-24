using BusinessTier.IHandler;
using DataTier.Context;
using DataTier.Models;
using DataTier.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessTier.Handlers
{
    public class CustomerQueryHandler :
        IQueryHandler<FindCustomerById, Customer>,
        IQueryHandler<FindNewlyCreatedCustomers, IEnumerable<Customer>>,
        IQueryHandler<FindCustomersByNotReceivedMessage, IEnumerable<Customer>>
    {
        private readonly ILogger _logger;
        private readonly CosmosDbContext _context;
        public CustomerQueryHandler(
            ILogger<CustomerQueryHandler> logger,
            CosmosDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Customer> Handle(FindCustomerById query)
        {
            return await _context.FindAsync<Customer>(query.CustomerId);
        }

        public async Task<IEnumerable<Customer>> Handle(FindCustomersByNotReceivedMessage query)
        {
            return await _context.Customers
                .Where(c => c.ReceivedMessage == false)
                .ToListAsync();
        }

        public async Task<IEnumerable<Customer>> Handle(FindNewlyCreatedCustomers query)
        {
            try
            {
                var result = _context.Customers
                    .ToList()
                    .Where(customer => customer.MortgageOffer == null);

                return result;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
