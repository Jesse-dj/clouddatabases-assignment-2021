using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataTier;
using DataTier.Models;
using DataTier.Queries;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace CalculateMortgageAndSendMail.Functions
{
    public class SendMail
    {
        private readonly IMediator _mediator;
        private readonly string sendGridApiKey;

        public SendMail(IMediator mediator)
        {
            _mediator = mediator;
            sendGridApiKey = Environment.GetEnvironmentVariable("sendgrid_key");
        }

        [Function("SendMail")]
        public async Task Run([TimerTrigger("0 0 8 * * *")] MyInfo myTimer, FunctionContext context)
        {
            var logger = context.GetLogger("SendMail");

            var query = new GetCustomersByNewlyCreated();
            IEnumerable<Customer> customers = await _mediator.Send(query);

            foreach (Customer customer in customers)
            {
                await SendMailAsync(customer, customer.MortgageOffer);
            }
            logger.LogInformation($"Total Mails Send: {customers.Count()}");

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
