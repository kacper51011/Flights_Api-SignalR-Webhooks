using Flights.Application.Dtos;
using Flights.Application.Messages;
using Flights.Application.WebhookService;
using Flights.Domain.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Flights.Application.Consumers
{
    public class FlightAddedOrChangedConsumer : IConsumer<FlightAddedOrChanged>
    {
        private readonly IWebhookService _webhookService;
        private readonly IFlightsRepository _flightRepository;
        private readonly IWebhookSubscriptionsRepository _webhookSubscriptionsRepository;
        private readonly ILogger<FlightAddedOrChangedConsumer> _logger;

        public FlightAddedOrChangedConsumer(ILogger<FlightAddedOrChangedConsumer> logger, IWebhookService webhookService, IFlightsRepository flightRepository, IWebhookSubscriptionsRepository webhookSubscriptionsRepository)
        {
            _webhookService = webhookService;
            _flightRepository = flightRepository;
            _webhookSubscriptionsRepository = webhookSubscriptionsRepository;
            _logger = logger;

        }
        public async Task Consume(ConsumeContext<FlightAddedOrChanged> context)
        {
            try
            {
                var flight = await _flightRepository.GetFlightById(context.Message.FlightId);
                if (flight == null)
                {
                    _logger.LogWarning($"Couldn`t find flight with Id {context.Message.FlightId}");
                    return;
                }

                var dtoToSend = new WebhookSendDataDto()
                {
                    FlightId = flight.FlightId,
                    FlightCompleted = flight.FlightCompleted,
                    FlightStarted = flight.FlightStarted,
                    From = flight.From,
                    To = flight.To,
                    StartTime = flight.StartTime,
                    EndTime = flight.EndTime,
                    Duration = flight.Duration,
                    Delay = flight.Delay,
                    DateOfUpdate = flight.UpdatedAt

                };


                var subscribers = await _webhookSubscriptionsRepository.GetAllSubscriptions();
                foreach (var subscriber in subscribers)
                {
                    dtoToSend.Secret = subscriber.Secret;
                    await _webhookService.NotifyAsync(subscriber.WebhookUri, dtoToSend);
                    _logger.LogInformation($"Webhook sent to: {subscriber.WebhookUri}");
                };

            }
            catch (Exception)
            {

                _logger.LogWarning($"Something went wrong in FlightAddedConsumer with  FlightId: {context.Message.FlightId}");
            }



        }
    }
}
