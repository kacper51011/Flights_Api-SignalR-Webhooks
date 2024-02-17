using Flights.Application.Commands.CreateFlight;
using Flights.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Flights_Api_SignalR_Webhooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FlightsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateFlight(CreateFlightDtoRequest createFlightDtoRequest)
        {
            var request = new CreateFlightCommand(createFlightDtoRequest);
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
