using Cqrs.Commands;
using DataTier;
using DataTier.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Store_API.Commands;
using Online_Store_API.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Store_API.Controllers
{
    public class EstateController : Controller
    {
        private readonly IMediator _mediator;
        public EstateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEstateAsync([FromRoute] GetEstateByIdQuery query)
        {
            var result = await _mediator.Send(query);
            return result == null ? NotFound(result) : Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetEstatesAsync([FromQuery] PriceRange? priceRange)
        {
            var query = GetEstatesAsync(priceRange);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
