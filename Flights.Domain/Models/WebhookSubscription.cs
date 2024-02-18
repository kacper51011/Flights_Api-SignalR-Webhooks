using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flights.Domain.Models
{
    public class WebhookSubscription
    {
        public string Id { get; set; }
        public string WebhookUri { get; set; }
        public string Secret { get; set; }

        public string WebhookType { get; set; }
        public string Publisher { get; set; }

    }
}
