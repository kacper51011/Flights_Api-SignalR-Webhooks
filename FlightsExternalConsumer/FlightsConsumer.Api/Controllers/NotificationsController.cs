using FlightsConsumer.Application.Commands;
using FlightsConsumer.Application.Dtos;
using FlightsConsumer.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlightsConsumer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IFlightsRepository _flightsRepository;
        private readonly IMediator _mediator;

        public NotificationsController(IFlightsRepository flightsRepository, IMediator mediator)
        {
            _flightsRepository = flightsRepository;
            _mediator = mediator;
        }

        [HttpPost]

        public async Task<ActionResult> Notify(IncomingWebhookDto incomingWebhookDto)
        {
            try
            {
                var command = new CreateOrUpdateFlightCommand(incomingWebhookDto);

                await _mediator.Send(command);

                return Created();
            }
            catch (Exception)
            {

                throw;
            }




        }

        [HttpGet]
        [Route("signalR/test/init")]
        public async Task<ActionResult<List<FlightSignalRDto>>> TestMethod()
        {
            try
            {
                var flights = await _flightsRepository.GetTenLastFlightsFromToday();
                var flightsDtos = new List<FlightSignalRDto>();

                foreach (var flight in flights)
                {
                    var flightSignalRDtoflight = flight.ToSignalRDto();
                    flightsDtos.Add(flightSignalRDtoflight);
                }
                return flightsDtos;
            }
            catch (Exception)
            {

                throw;
            }


        }

    }
}
