using System;
using System.IO;
using System.Threading.Tasks;
using DataTier;
using DataTier.IServices;
using DataTier.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Storage.Queue;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace CalculateMortgageAndSendMail.Functions
{
    public class SendMail
    {
        private readonly ICustomerService _service;
        private string sendGridApiKey;

        public SendMail(ICustomerService service)
        {
            _service = service;
            sendGridApiKey = Environment.GetEnvironmentVariable("sendgrid_key");
        }

        [Function("SendMail")]
        public async Task Run([TimerTrigger("0 0 8 * * *")] MyInfo myTimer, FunctionContext context)
        {
            var logger = context.GetLogger("SendMail");

            var customers = await _service.GetMultiple("");

            foreach (Customer customer in customers)
            {
                await SendMailAsync(customer, customer.MortgageOffer);
            }

            logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
        }

        private async Task SendMailAsync(Customer customer, MortgageOffer offer)
        {
            var client = new SendGridClient(sendGridApiKey);
            var message = new SendGridMessage();
            message.SetFrom("629393@student.inholland.nl", "BuyMyHouse");
            message.AddTo(customer.Email, customer.Firstname);
            message.SetSubject("[BuyMyHouse Estates] Calculated Mortgage");
            message.HtmlContent = $"Dear {customer.Firstname},</br>" +
                                $"Your total calculated mortgage is: € {offer.TotalMortgage}</br>" +
                                $"Your montlhy payment will be: € {offer.MonthlyPayments}</br>" +
                                "With kind regards,</br>" +
                                "The BuyMyHouse Group";

            var response = await client.SendEmailAsync(message);
            if (!response.IsSuccessStatusCode)
            {
                var responseBody = await response.Body.ReadAsStringAsync();
                Console.WriteLine(responseBody);              
            }
        }
    }
}
