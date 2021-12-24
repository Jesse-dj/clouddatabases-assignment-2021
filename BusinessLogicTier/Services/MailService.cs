using BusinessTier.IServices;
using DataTier.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace BusinessTier.Services
{
    public class MailService : IMessageService
    {
        private readonly IConfiguration _configuration;
        public ILogger<MailService> _logger { get; set; }
        public MailService(IConfiguration configuration, ILogger<MailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendMessage(string receiver, MortgageOffer offer)
        {
            string sendGridApiKey = _configuration.GetValue<string>("sendgrid_key");
            var client = new SendGridClient(sendGridApiKey);
            var message = new SendGridMessage();
            message.SetFrom("629393@student.inholland.nl", "BuyMyHouse");
            message.AddTo(receiver);
            message.SetSubject("[BuyMyHouse Estates] Calculated Mortgage");
            message.HtmlContent = $"Dear Customer,</br>" +
                                $"Your total calculated mortgage is: € {offer.TotalMortgage}</br>" +
                                $"Your montlhy payment will be: € {offer.MonthlyPayments}</br>" +
                                "With kind regards,</br>" +
                                "The BuyMyHouse Group";

            var response = await client.SendEmailAsync(message);

            if (!response.IsSuccessStatusCode)
            {
                var responseBody = await response.Body.ReadAsStringAsync();
                _logger.LogError($"Error Sending Email: {responseBody}");
                return;
            }
        }
    }
}
