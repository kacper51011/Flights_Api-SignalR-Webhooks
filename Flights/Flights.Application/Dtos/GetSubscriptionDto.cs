using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flights.Application.Dtos
{
    public class GetSubscriptionDto
    {
        public string Id { get; set; }
        public string WebhookUri { get; set; }
        public string Secret { get; set; }
    }
}
