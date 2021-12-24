using BusinessTier.Handlers;
using BusinessTier.IHandler;
using BusinessTier.IServices;
using DataTier.Commands;
using DataTier.Models;
using DataTier.Queries;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculateMortgageAndSendMail.Functions
{
    public class SendMail
    {
        private readonly ILogger<SendMail> _logger;
        private readonly IMessageService _messageService;
        private readonly IQueryHandler<FindCustomersByNotReceivedMessage, IEnumerable<Customer>> _queryHandler;
        private readonly CustomerCommandHandler _commandHandler;

        public SendMail(
            ILogger<SendMail> logger,
            IMessageService messageService,
            CustomerQueryHandler queryHandler,
            CustomerCommandHandler commandHandler)
        {
            _logger = logger;
            _messageService = messageService;
            _queryHandler = queryHandler;
            _commandHandler = commandHandler;
        }

        [Function("SendMail")]
        public async Task Run([TimerTrigger("0 0 8 * * *")] MyInfo myTimer, FunctionContext context)
        {
            var query = new FindCustomersByNotReceivedMessage();
            IEnumerable<Customer> customers = await _queryHandler.Handle(query);

            foreach (Customer customer in customers)
            {
                await _messageService.SendMessage(customer.Email, customer.MortgageOffer);
                var command = new CustomerReceivedMessage(customer.CustomerId);
                await _commandHandler.Handle(command);
            }

            _logger.LogInformation($"Total Mails Send: {customers.Count()}");

            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
        }
    }
}
