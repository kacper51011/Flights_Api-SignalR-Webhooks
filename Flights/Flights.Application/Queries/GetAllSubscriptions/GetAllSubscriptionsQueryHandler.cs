using Flights.Application.Dtos;
using Flights.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flights.Application.Queries.GetAllSubscriptions
{
    public class GetAllSubscriptionsQueryHandler : IRequestHandler<GetAllSubscriptionsQuery, List<GetSubscriptionDto>>
    {
        private readonly IWebhookSubscriptionsRepository _webhookSubscriptionsRepository;

        public GetAllSubscriptionsQueryHandler(IWebhookSubscriptionsRepository webhookSubscriptionsRepository)
        {
            _webhookSubscriptionsRepository = webhookSubscriptionsRepository;
        }

        public async Task<List<GetSubscriptionDto>> Handle(GetAllSubscriptionsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new List<GetSubscriptionDto>();
                var webhooks = await _webhookSubscriptionsRepository.GetAllSubscriptions();
                foreach ( var webhook in webhooks )
                {
                    response.Add( new GetSubscriptionDto() { Id = webhook.Id, Secret = webhook.Secret, WebhookUri= webhook.WebhookUri });
                }
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
