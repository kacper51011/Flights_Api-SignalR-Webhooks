using Flights.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flights.Domain.Interfaces
{
    public interface IWebhookSubscriptionsRepository
    {
        public Task<WebhookSubscription> GetSubscriptionById(string subscriptionId);
        public Task<WebhookSubscription> GetSubscriptionByUri(string uri);
        public Task<List<WebhookSubscription>> GetAllSubscriptions();
        public Task AddSubscription(WebhookSubscription subscription);
    }
}
