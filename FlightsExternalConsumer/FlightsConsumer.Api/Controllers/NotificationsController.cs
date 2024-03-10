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
        private readonly IMediator _mediator;

        public NotificationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]

        public async Task<ActionResult> Notify(IncomingWebhookDto incomingWebhookDto)
        {
            try
            {
                var command = new CreateOrUpdateFlightCommand(incomingWebhookDto);

                await _mediator.Send(command);

                return Ok();
            }
            catch(UnauthorizedAccessException ex)
            {
                return StatusCode(401, ex.Message);
            }
            catch (Exception)
            {

                throw;
            }




        }

    }
}
