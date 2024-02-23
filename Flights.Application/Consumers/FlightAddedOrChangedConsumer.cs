using Flights.Application.Dtos;
using Flights.Application.Messages;
using Flights.Application.WebhookService;
using Flights.Domain.Interfaces;
using MassTransit;

namespace Flights.Application.Consumers
{
    public class FlightAddedOrChangedConsumer : IConsumer<FlightAddedOrChanged>
    {
        private readonly IWebhookService _webhookService;
        private readonly IFlightsRepository _flightRepository;
        private readonly IWebhookSubscriptionsRepository _webhookSubscriptionsRepository;

        public FlightAddedOrChangedConsumer(IWebhookService webhookService, IFlightsRepository flightRepository, IWebhookSubscriptionsRepository webhookSubscriptionsRepository)
        {
            _webhookService = webhookService;
            _flightRepository = flightRepository;
            _webhookSubscriptionsRepository = webhookSubscriptionsRepository;

        }
        public async Task Consume(ConsumeContext<FlightAddedOrChanged> context)
        {
            try
            {
                var flight = await _flightRepository.GetFlightById(context.Message.FlightId);
                if (flight == null)
                {
                    return;
                }

                var dtoToSend = new WebhookSendDataDto()
                {
                    FlightId = flight.FlightId,
                    FlightCompleted = flight.FlightCompleted,
                    FlightStarted = flight.FlightStarted,
                    From = flight.From,
                    To = flight.To,
                    StartTime = flight.StartTime.Value,
                    EndTime = flight.EndTime.Value,
                    Duration = flight.Duration.Value,
                    Delay = flight.Delay,

                };


                var subscribers = await _webhookSubscriptionsRepository.GetAllSubscriptions();
                foreach (var subscriber in subscribers)
                {
                    dtoToSend.Secret = subscriber.Secret;
                    await _webhookService.NotifyAsync(subscriber.WebhookUri, dtoToSend);
                };
            }
            catch (Exception)
            {

                throw;
            }



        }
    }
}
