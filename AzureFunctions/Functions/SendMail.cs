using System;
using System.Threading.Tasks;
using DataTier;
using DataTier.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Storage.Queue;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace FunctionApp1
{
    public static class SendMail
    {
        [Function("SendMail")]
        public static async Task Run([TimerTrigger("0 0 8 * * *")] MyInfo myTimer, FunctionContext context)
        {
            var logger = context.GetLogger("SendMail");

            StorageAccount storageAccount = new StorageAccount();
            var queueClient = storageAccount.CreateCloudQueueClient();
            var cloudQueue = queueClient.GetQueueReference("mortgageoffer");
            await cloudQueue.CreateIfNotExistsAsync();

            if (!cloudQueue.Exists())
            {
                logger.LogError($"Cloud Queue {cloudQueue.Name} Did not Exist");
                return;
            }
            var customer = new Customer();
            var offer = new MortgageOffer(customer.IncomePerYear);
            await SendMailAsync(customer, offer);

            logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
        }

        private static async Task SendMailAsync(Customer customer, MortgageOffer offer)
        {
            var apiKey = Environment.GetEnvironmentVariable("sendgrid_key");
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("info@buymyhouse.com", "BuyMyHouse"),
                Subject = "[BuyMyHouse Estates] Calculated Mortgage",
                HtmlContent = $"Your calculated mortgage is {offer.TotalMortgage} \n" +
                                $"Your montlhy payment will be {offer.MonthlyPayments}"
            };
            msg.AddTo(new EmailAddress(customer.Email, customer.Firstname));
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Body.ToString());
            }
            return;
        }
    }
}
