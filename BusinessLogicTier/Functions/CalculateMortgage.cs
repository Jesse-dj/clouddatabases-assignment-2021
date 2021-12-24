using BusinessTier.Handlers;
using BusinessTier.IServices;
using DataTier.Commands;
using DataTier.Models;
using DataTier.Queries;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CalculateMortgageAndSendMail.Functions
{
    public class CalculateMortgage
    {
        private readonly IMortgageService _mortgageService;
        private readonly CustomerCommandHandler _commandHandler;
        private readonly CustomerQueryHandler _queryHandler;

        public CalculateMortgage(
            IMortgageService mortgageService,
            CustomerCommandHandler commandHandler,
            CustomerQueryHandler queryHandler)
        {
            _commandHandler = commandHandler;
            _queryHandler = queryHandler;
            _mortgageService = mortgageService;
        }

        [Function("CalculateMortgage")]
        public async Task Run([TimerTrigger("0 0 0 * * *")] MyInfo myTimer, FunctionContext context)
        {
            var logger = context.GetLogger("CalculateMortgage");

            var query = new FindNewlyCreatedCustomers();
            var result = await _queryHandler.Handle(query);

            foreach (Customer customer in result)
            {
                MortgageOffer offer = _mortgageService.CalculateMortgage(customer.YearlyIncome);
                var command = new AddMortgageOffer(customer.CustomerId, offer);
                await _commandHandler.Handle(command);
            }

            logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
        }
    }

    public class MyInfo
    {
        public ScheduleStatus ScheduleStatus { get; set; }

        public bool IsPastDue { get; set; }
    }

    public class MySchedueleStatus
    {
        public DateTime Last { get; set; }

        public DateTime Next { get; set; }

        public DateTime LastUpdated { get; set; }

    }
}
