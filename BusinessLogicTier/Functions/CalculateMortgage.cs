using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataTier;
using DataTier.Commands;
using DataTier.Models;
using DataTier.Queries;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CalculateMortgageAndSendMail.Functions
{
    public class CalculateMortgage
    {
        private readonly IMediator _mediator;
        public CalculateMortgage(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Function("CalculateMortgage")]
        public async Task Run([TimerTrigger("0 0 0 * * *")] MyInfo myTimer, FunctionContext context)
        {
            var logger = context.GetLogger("CalculateMortgage");

            var query = new GetCustomersByNoOffer();
            IEnumerable<Customer> customers = await _mediator.Send(query);

            foreach (var customer in customers)
            {
                customer.MortgageOffer = new MortgageOffer(customer.IncomePerYear);
                var command = new UpdateCustomer()
                {
                    Customer = customer
                };
                await _mediator.Send(command);
            }
            logger.LogInformation($"Total Mortgages Calculated: {customers.Count()}");

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
