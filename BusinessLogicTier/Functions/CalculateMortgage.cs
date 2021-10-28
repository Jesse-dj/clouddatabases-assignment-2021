using System;
using System.Threading.Tasks;
using DataTier;
using DataTier.IServices;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CalculateMortgageAndSendMail.Functions
{
    public class CalculateMortgage
    {
        private readonly ICustomerService _customerService;
        
        public CalculateMortgage(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [Function("CalculateMortgage")]
        public async Task Run([TimerTrigger("0 0 0 * * *")] MyInfo myTimer, FunctionContext context)
        {
            var logger = context.GetLogger("CalculateMortgage");

            var customers = await _customerService.FindBy(c => c.MortgageOffer == null);

            foreach (var customer in customers)
            {
                customer.MortgageOffer = new MortgageOffer(customer.IncomePerYear);
                await _customerService.Update(customer);
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
