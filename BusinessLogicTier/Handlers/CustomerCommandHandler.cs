using BusinessTier.IHandler;
using DataTier.Commands;
using DataTier.Context;
using DataTier.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BusinessTier.Handlers
{
    public class CustomerCommandHandler :
        ICommandHandler<AddNewCustomer>,
        ICommandHandler<AddMortgageOffer>,
        ICommandHandler<CustomerReceivedMessage>
    {
        private readonly ILogger _logger;
        private readonly CosmosDbContext _context;

        public CustomerCommandHandler(
            ILogger<CustomerCommandHandler> logger,
            CosmosDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task Handle(AddNewCustomer command)
        {
            try
            {
                var newCustomer = new Customer()
                {
                    Firstname = command.Firstname,
                    Lastname = command.Lastname,
                    Email = command.Email,
                    YearlyIncome = command.YearlyIncome
                };

                await _context.AddAsync(newCustomer);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException updateException)
            {
                _logger.LogError($"Add Customer Exception: {updateException.Message}");
                throw;
            }

        }

        public async Task Handle(AddMortgageOffer command)
        {
            try
            {
                var customer = await _context.FindAsync<Customer>(command.customerId);

                customer.MortgageOffer = command.mortgageOffer;

                _context.Update(customer);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException updateException)
            {
                _logger.LogError($"Add Mortgage Exception: {updateException.Message}");
                throw;
            }
        }

        public async Task Handle(CustomerReceivedMessage command)
        {
            try
            {
                var customer = await _context.FindAsync<Customer>(command.CustomerId);

                customer.ReceivedMessage = true;

                _context.Update(customer);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException updateException)
            {
                _logger.LogError($"Customer Received Message Exception: {updateException.Message}");
                throw;
            }
        }
    }
}
