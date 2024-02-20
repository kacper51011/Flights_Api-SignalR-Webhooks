using Flights.Application.Exceptions;
using Flights.Domain.Interfaces;
using Flights.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flights.Application.Commands.CreateSubscription
{
    public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, string>
    {
        private readonly IWebhookSubscriptionsRepository _webhookSubscriptionsRepository;
        public async Task<string> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _webhookSubscriptionsRepository.GetSubscriptionByUri(request.createSubscriptionDto.WebhookUri);
                if (response != null)
                {
                    throw new AlreadyExistsException("Subscription for that uri already exists!");
                }

                WebhookSubscription subscription = new(request.createSubscriptionDto.WebhookUri);
                await _webhookSubscriptionsRepository.AddSubscription(subscription);

                return subscription.Secret;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
