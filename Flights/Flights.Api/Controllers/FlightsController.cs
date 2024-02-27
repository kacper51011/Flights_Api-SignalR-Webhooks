using Flights.Application.Commands.CreateFlight;
using Flights.Application.Commands.DecrementFlightDelay;
using Flights.Application.Commands.IncrementFlightDelay;
using Flights.Application.Dtos;
using Flights.Application.Queries.GetFlightById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Flights_Api_SignalR_Webhooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        // Endpoints created with CQRS in mind
        private readonly IMediator _mediator;
        public FlightsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("{id}")]

        public async Task<ActionResult<GetFlightResponseDto>> GetFlightById(string id)
        {
            try
            {
                var request = new GetFlightByIdQuery(id);
                var response = await _mediator.Send(request);
                return Ok(response);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("CreateFlight")]
        public async Task<ActionResult<string>> CreateFlight(CreateFlightDtoRequest createFlightDtoRequest)
        {
            try
            {
                var request = new CreateFlightCommand(createFlightDtoRequest);
                var response = await _mediator.Send(request);
                return Ok(response);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPost]
        [Route("Flights/{id}/IncrementDelay")]
        public async Task<ActionResult> IncrementFlightDelay(string id, TimeSpan incrementDelayValue)
        {
            try
            {
                var request = new IncrementFlightDelayCommand(id, incrementDelayValue);
                await _mediator.Send(request);
                return Ok();
            }
            catch (Exception)
            {

                return StatusCode(500);
            }

        }

        [HttpPost]
        [Route("Flights/{id}/DecrementDelay")]
        public async Task<ActionResult> DecrementFlightDelay(string id, TimeSpan decrementDelayValue)
        {
            try
            {
                var request = new DecrementFlightDelayCommand(id, decrementDelayValue);
                await _mediator.Send(request);
                return Ok();
            }
            catch (Exception)
            {

                return StatusCode(500);
            }

        }


    }
}
