using System;
using System.Threading.Tasks;
using DataTier;
using DataTier.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Storage.Queue;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FunctionApp1
{
    public static class CalculateMortgage
    { 
        [Function("CalculateMortgage")]
        public static async Task Run([TimerTrigger("0 0 0 * * *")] MyInfo myTimer, FunctionContext context)
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
                var customer = JsonConvert.DeserializeObject<Customer>(message.AsString);
                var offer = new MortgageOffer(customer.IncomePerYear);
                var message = new CloudQueueMessage(offer.ToString());
                await cloudQueue.AddMessageAsync(message);
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
