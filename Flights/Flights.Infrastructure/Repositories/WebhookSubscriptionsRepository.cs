using Flights.Domain.Interfaces;
using Flights.Domain.Models;
using Flights.Infrastructure.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flights.Infrastructure.Repositories
{
    public class WebhookSubscriptionsRepository : IWebhookSubscriptionsRepository
    {
        private readonly ApplicationDbContext _context;

        public WebhookSubscriptionsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddSubscription(WebhookSubscription subscription)
        {
            await _context.WebhookSubscriptions.AddAsync(subscription);
        }

        public async Task<List<WebhookSubscription>> GetAllSubscriptions()
        {
            var response = await _context.WebhookSubscriptions.ToListAsync();
            return response;
        }

        public async Task<WebhookSubscription> GetSubscriptionById(string subscriptionId)
        {
            var response = await _context.WebhookSubscriptions.FirstOrDefaultAsync(x => x.Id == subscriptionId);
            return response;
        }

        public async Task<WebhookSubscription> GetSubscriptionByUri(string uri)
        {
            var response = await _context.WebhookSubscriptions.FirstOrDefaultAsync(x => x.WebhookUri == uri);
            return response;

        }

        public async Task<WebhookSubscription> GetSubscriptionBySecret(string secret)
        {
            var response = await _context.WebhookSubscriptions.FirstOrDefaultAsync(x => x.Secret == secret);
            return response;

        }

        public async Task DeleteAllSubscriptionsForTest()
        {
            await _context.WebhookSubscriptions.ExecuteDeleteAsync();
        }

        public void DeleteSubscription(WebhookSubscription subscription)
        {
            _context.WebhookSubscriptions.Remove(subscription);
        }
    }
}
