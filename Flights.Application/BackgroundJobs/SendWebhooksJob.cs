using Flights.Application.Messages;
using Flights.Domain.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Flights.Application.BackgroundJobs
{
    public class SendWebhooksJob : IJob
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IFlightsRepository _flightsRepository;
        private readonly ILogger<SendWebhooksJob> _logger;
        public SendWebhooksJob(IPublishEndpoint publishEndpoint, IFlightsRepository flightsRepository, ILogger<SendWebhooksJob> logger)
        {
            _publishEndpoint = publishEndpoint;
            _flightsRepository = flightsRepository;
            _logger = logger;

        }
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var notSendedFlights = await _flightsRepository.GetNotSendedFlights();
                if (notSendedFlights == null)
                {
                    _logger.LogInformation("No updated or created flights to send to subscribers");
                    return;
                }


                foreach (var flight in notSendedFlights)
                {
                    await _publishEndpoint.Publish(new FlightAddedOrChanged { FlightId = flight.FlightId });
                }

            }
            catch (Exception)
            {
                _logger.LogWarning("Something went wrong in SendWebhooksJob");
                
            }

        }
    }
}
