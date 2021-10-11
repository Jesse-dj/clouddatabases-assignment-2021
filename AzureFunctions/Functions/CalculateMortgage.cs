using System;
using System.Threading.Tasks;
using DataTier;
using DataTier.Models;
using DataTier.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Storage.Queue;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FunctionApp1
{
    public class CalculateMortgage
    {
        private readonly IDbService<Customer> _customerService;
        private IDbService<MortgageOffer> _offerService;
        public CalculateMortgage(CustomerDbService customerService, OfferDbService offerService)
        {
            _customerService = customerService;
            _offerService = offerService;
        }

        [Function("CalculateMortgage")]
        public async Task Run([TimerTrigger("0 0 0 * * *")] MyInfo myTimer, FunctionContext context)
        {
            var logger = context.GetLogger("CalculateMortgage");

            var queueStorage = new QueueStorage();
            var cloudQueue = await queueStorage.GetQueueAsync("customerinfo");

            if (!cloudQueue.Exists())
            {
                logger.LogError($"Cloud Queue {cloudQueue.Name} Did not Exist");
                return;
            }

            int messageCount = cloudQueue.ApproximateMessageCount ?? 0;
            var messages = await cloudQueue.GetMessagesAsync(messageCount);

            foreach (var message in messages)
            {
                Customer customer = await _customerService.GetAsync(message.AsString);
                MortgageOffer offer = await _offerService.AddAsync(new MortgageOffer(customer.IncomePerYear));
                customer.MortgageOffer = offer;
                await _customerService.UpdateAsync(customer);
                var newCloudMessage = new CloudQueueMessage(offer.ToString());
                await cloudQueue.AddMessageAsync(newCloudMessage);
            }

            logger.LogInformation($"Processed {messageCount} Offers at {DateTime.Now}");

            logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
        }
    }

    public class MyInfo
    {
        public MyScheduleStatus ScheduleStatus { get; set; }

        public bool IsPastDue { get; set; }
    }

    public class MyScheduleStatus
    {
        public DateTime Last { get; set; }

        public DateTime Next { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
