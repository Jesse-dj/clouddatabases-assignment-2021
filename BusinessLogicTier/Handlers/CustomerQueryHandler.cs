using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataTier.Context;
using DataTier.Models;
using DataTier.Queries;
using MediatR;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BusinessTier.Handlers
{
    public class CustomerQueryHandler :
        IRequestHandler<GetCustomerById, Customer>,
        IRequestHandler<GetCustomersByNoOffer, IEnumerable<Customer>>,
        IRequestHandler<GetCustomersByNewlyCreated, IEnumerable<Customer>>
    {
        private readonly ILogger _logger;
        public CustomerQueryHandler(ILogger<CustomerQueryHandler> logger)
        {
            _logger = logger;
        }

        public async Task<Customer> Handle(GetCustomerById request, CancellationToken cancellationToken)
        {
            try
            {
                using (var context = new CosmosDbContext())
                {
                    return await context.FindAsync<Customer>(request.CustomerId);
                }               
            }
            catch (CosmosException cosmosException)
            {
                _logger.LogWarning(cosmosException.Message);
                return null;
            }
        }

        public async Task<IEnumerable<Customer>> Handle(GetCustomersByNoOffer request, CancellationToken cancellationToken)
        {
            try
            {
                using (var context = new CosmosDbContext())
                {
                    // For some reason the linq equivalent doesnt work

                    // works
                    var query = context.Customers
                        .FromSqlRaw(request.HasNoMortgageOfferQuery);

                    // doenst work
                    /*var query = context.Customers.Where(c => c.MortgageOffer == null);*/

                    return await query.ToListAsync();
                }
            }
            catch (CosmosException cosmosException)
            {
                _logger.LogWarning(cosmosException.Message);
                return Enumerable.Empty<Customer>();
            }

        }

        public async Task<IEnumerable<Customer>> Handle(GetCustomersByNewlyCreated request, CancellationToken cancellationToken)
        {
            try
            {
                using (var context = new CosmosDbContext())
                {
                    // does work
                    var query = context.Customers
                            .Where(c => c.MortgageOffer != null)
                            .Where(request.offerMadeWithin24Hours);

                    return await query.ToListAsync();
                }
            }
            catch (CosmosException cosmosException)
            {
                _logger.LogWarning(cosmosException.Message);
                return Enumerable.Empty<Customer>();
            }
        }
    }
}
