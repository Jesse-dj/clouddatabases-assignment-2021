using AutoMapper;
using DataTier.Commands;
using DataTier.Context;
using DataTier.Models;
using MediatR;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessTier.Handlers
{
    public class CustomerCommandHandler :
        IRequestHandler<AddNewCustomer, Customer>,
        IRequestHandler<UpdateCustomer, Customer>
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public CustomerCommandHandler(ILogger<CustomerCommandHandler> logger)
        {
            _logger = logger;

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<CustomerDTO, Customer>();
            });
            _mapper = config.CreateMapper();
        }

        public async Task<Customer> Handle(UpdateCustomer request, CancellationToken cancellationToken)
        {
            try
            {
                using (var context = new CosmosDbContext())
                {
                    await context.Database.EnsureCreatedAsync();

                    var result = context.Update<Customer>(request.Customer);
                    await context.SaveChangesAsync(cancellationToken);
                    return await Task.FromResult(result.Entity);
                }
            }
            catch (CosmosException cosmosException)
            {
                _logger.LogWarning(cosmosException.Message);
                return null;
            }

        }

        async Task<Customer> IRequestHandler<AddNewCustomer, Customer>.Handle(AddNewCustomer request, CancellationToken cancellationToken)
        {
            try
            {
                using(var context = new CosmosDbContext())
                {
                    await context.Database.EnsureCreatedAsync();

                    Customer newCustomer = _mapper.Map<CustomerDTO, Customer>(request.Customer);
                    var result = await context.AddAsync(newCustomer, cancellationToken);
                    await context.SaveChangesAsync(cancellationToken);
                    return result.Entity;
                }
            }
            catch (CosmosException cosmosException)
            {
                _logger.LogWarning(cosmosException.Message);
                return null;
            }

        }
    }
}
