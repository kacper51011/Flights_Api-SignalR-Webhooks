using Flights.Application.Commands.CreateSubscription;
using Flights.Application.Dtos;
using Flights.Application.Exceptions;
using Flights.Application.Queries.GetAllSubscriptions;
using Flights.Domain.Exceptions;
using Flights.Domain.Interfaces;
using Flights.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Flights_Api_SignalR_Webhooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubscriptionsController(IMediator mediator, IWebhookSubscriptionsRepository subRepository)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("CreateSubscription")]
        public async Task<ActionResult<string>> CreateSubscription(CreateSubscriptionDto dto)
        {
            try
            {
                var command = new CreateSubscriptionCommand(dto);
                var response = await _mediator.Send(command);
                return Ok(response);
            }

            catch (AlreadyExistsException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }
    }
}
