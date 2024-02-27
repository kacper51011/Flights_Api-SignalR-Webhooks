using Flights.Application.Messages;
using Flights.Domain.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flights.Application.BackgroundJobs
{
    [DisallowConcurrentExecution]
    public class WebhookJob : IJob
    {
        private readonly ILogger<WebhookJob> _logger;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IFlightsRepository _flightsRepository;
        public WebhookJob(ILogger<WebhookJob> logger, IPublishEndpoint publishEndpoint, IFlightsRepository flightsRepository)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
            _flightsRepository = flightsRepository;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var notSendedFlights = await _flightsRepository.GetNotSendedFlights();
                if (notSendedFlights.Count == 0)
                {
                    _logger.LogWarning("No updated or created flights to send to subscribers");
                    return;
                }

                foreach (var flight in notSendedFlights)
                {
                    await _publishEndpoint.Publish(new FlightAddedOrChanged { FlightId = flight.FlightId });
                    _logger.LogWarning($"{flight.FlightId} sent through flightAddedOrChanged");

                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Something went wrong in SendWebhooksJob");

            }
        }
    }
}
