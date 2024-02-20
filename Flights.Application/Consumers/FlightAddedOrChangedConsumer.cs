using Flights.Application.Messages;
using Flights.Domain.Interfaces;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Flights.Application.Consumers
{
    public class FlightAddedOrChangedConsumer : IConsumer<FlightAddedOrChanged>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IFlightsRepository _flightRepository;
        private readonly IWebhookSubscriptionsRepository _webhookSubscriptionsRepository;

        public FlightAddedOrChangedConsumer(IHttpClientFactory httpClientFactory, IFlightsRepository flightRepository, IWebhookSubscriptionsRepository webhookSubscriptionsRepository)
        {
            _httpClientFactory = httpClientFactory;
            _flightRepository = flightRepository;
            _webhookSubscriptionsRepository = webhookSubscriptionsRepository;
            
        }
        public Task Consume(ConsumeContext<FlightAddedOrChanged> context)
        {
            throw new NotImplementedException();
        }
    }
}
