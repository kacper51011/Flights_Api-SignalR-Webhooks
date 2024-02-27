using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flights.Domain.Models
{
    public class WebhookSubscription
    {
        public WebhookSubscription(string webhookUri)
        {
            Id = Guid.NewGuid().ToString();
            WebhookUri = webhookUri;
            // secret is in a form of guid only to simplify the dataflow
            Secret = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string WebhookUri { get; set; }
        public string Secret { get; set; }

    }
}
