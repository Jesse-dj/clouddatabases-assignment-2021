using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CalculateMortgageAndSendMail.Functions
{
    public static class ClearOfferData
    {
        [Function("ClearOfferData")]
        public static void Run([TimerTrigger("0 55 7 * * *")] MyInfo myTimer, FunctionContext context)
        {
            var logger = context.GetLogger("ClearOfferData");
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
