using DataTier.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Online_Store_API.Controllers
{
    [Route("/customers")]
    public class CustomerController : Controller
    {
        private IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] AddNewCustomerCommand command)
        {
            var result = await _mediator.Send(command);
            var uri = $"{Request.Path}/{result.id}";
            return Created(uri, result);
        }
    }
}
