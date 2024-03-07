using Flights.Application.Commands.CreateSubscription;
using Flights.Application.Dtos;
using Flights.Application.Queries.GetAllSubscriptions;
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
        private readonly IWebhookSubscriptionsRepository _subRepository;

        public SubscriptionsController(IMediator mediator, IWebhookSubscriptionsRepository subRepository)
        {
            _mediator = mediator;
            _subRepository = subRepository;
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
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("TestSubscriptions")]

        public async Task<ActionResult<List<WebhookSubscription>>> GetSubscriptionsTest()
        {
            try
            {
                var query = new GetAllSubscriptionsQuery();
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete]
        [Route("DeleteAll")]
        public async Task<ActionResult> DeleteAllForTest()
        {
            try
            {
                var list = await _subRepository.GetAllSubscriptions();
                foreach (var subscription in list)
                {
                    await _subRepository.DeleteAllSubscriptionsForTest();
                }
                return Ok(list);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
