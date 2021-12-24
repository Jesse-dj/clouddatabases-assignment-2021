using BusinessTier.Handlers;
using DataTier.Commands;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CalculateMortgageAndSendMail.Controllers
{
    public class CustomerController
    {
        public ILogger<CustomerController> _logger { get; }
        private readonly CustomerCommandHandler _handler;

        public CustomerController(
            ILogger<CustomerController> logger,
            CustomerCommandHandler handler)
        {
            _logger = logger;
            _handler = handler;
        }


        [Function(nameof(CustomerController.AddCustomer))]
        public async Task<HttpResponseData> AddCustomer([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "customers")]
            HttpRequestData req,
            FunctionContext executionContext)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await req.ReadAsStringAsync();
            AddNewCustomer command = JsonConvert.DeserializeObject<AddNewCustomer>(requestBody);

            await _handler.Handle(command);
            var response = req.CreateResponse();

            await response.WriteAsJsonAsync("Customer created successfully");
            return response;
        }
    }
}
