using Flights.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flights.Application.WebhookService
{
    public interface IWebhookService
    {
        public Task NotifyAsync(string url, WebhookSendDataDto dto);
    }
}
