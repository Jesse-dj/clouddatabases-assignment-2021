using System.IO;
using System.Threading.Tasks;
using DataTier.Commands;
using DataTier.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CalculateMortgageAndSendMail.Controllers
{
    public class CustomerController
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Function("CustomerController")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "customers")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("CustomerController");
            logger.LogInformation("C# HTTP trigger function processed a request.");

            var requestBody = string.Empty;
            using (StreamReader streamReader = new(req.Body))
            {
                requestBody = await streamReader.ReadToEndAsync();
            }
            CustomerDTO customer = JsonConvert.DeserializeObject<CustomerDTO>(requestBody);
            var command = new AddNewCustomer()
            {
                Customer = customer
            };

            var result = await _mediator.Send(command);

            return new OkObjectResult(result);
        }
    }
}
