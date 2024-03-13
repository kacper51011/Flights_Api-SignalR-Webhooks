using FlightsConsumer.Application.Messages;
using FlightsConsumer.Application.Secrets;
using FlightsConsumer.Domain.Entities;
using FlightsConsumer.Domain.Interfaces;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Options;

namespace FlightsConsumer.Application.Commands
{
    public class CreateOrUpdateFlightCommandHandler : IRequestHandler<CreateOrUpdateFlightCommand>
    {
        private readonly IFlightsRepository _flightsRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly string _webhookKey;
        public CreateOrUpdateFlightCommandHandler(IPublishEndpoint publishEndpoint, IFlightsRepository flightsRepository ,IOptions<SecretKeys> secretKeys)
        {
            _publishEndpoint = publishEndpoint;
            _flightsRepository = flightsRepository;
            _webhookKey = secretKeys.Value.WebhookKey;

        }
        public async Task Handle(CreateOrUpdateFlightCommand request, CancellationToken cancellationToken)
        {
            // check if secret is correct to prevent unauthorized senders
            if (request.dto.Secret != _webhookKey)
            {
                throw new UnauthorizedAccessException("wrong secret key");
            }

            var x = request.dto;
            var existingFlight = await _flightsRepository.GetFlightById(request.dto.FlightId);
            if (existingFlight == null)
            {
                await _publishEndpoint.Publish(new FlightCreated { FlightId = x.FlightId, DateOfUpdate = x.DateOfUpdate, Delay= x.Delay, Duration= x.Duration, EndTime = x.EndTime, FlightCompleted= x.FlightCompleted, FlightStarted= x.FlightStarted, From = x.From, StartTime= x.StartTime, To = x.To });
            }
            else
            {
                //message from publisher can be sent more than once, so we check only the actual information based on dateOfUpdate
                if (request.dto.DateOfUpdate <= existingFlight.UpdatedInPublisherAt)
                {
                    return;
                }
                else
                {
                    await _publishEndpoint.Publish(new FlightUpdated { FlightId = x.FlightId, DateOfUpdate = x.DateOfUpdate, Delay = x.Delay, Duration = x.Duration, EndTime = x.EndTime, FlightCompleted = x.FlightCompleted, FlightStarted = x.FlightStarted, From = x.From, StartTime = x.StartTime, To = x.To });

                }
            }


        }
    }
}
