using FlightsConsumer.Domain.Entities;
using FlightsConsumer.Domain.Interfaces;
using MediatR;

namespace FlightsConsumer.Application.Commands
{
    public class CreateOrUpdateFlightCommandHandler : IRequestHandler<CreateOrUpdateFlightCommand>
    {
        private readonly IFlightsRepository _flightRepository;
        public CreateOrUpdateFlightCommandHandler(IFlightsRepository flightRepository)
        {
            _flightRepository = flightRepository;

        }
        public async Task Handle(CreateOrUpdateFlightCommand request, CancellationToken cancellationToken)
        {
            // check if secret is correct to prevent unauthorized senders
            if (request.dto.Secret != "actuallyRandomplaceholdervalue")
            {
                throw new UnauthorizedAccessException("wrong secret key");
            }

            var createdFlight = Flight.Create(request.dto.FlightId, request.dto.StartTime, request.dto.EndTime, request.dto.Duration, request.dto.Delay, request.dto.From, request.dto.To, request.dto.FlightStarted, request.dto.FlightCompleted, request.dto.DateOfUpdate);

            var existingFlight = await _flightRepository.GetFlightById(request.dto.FlightId);
            if (existingFlight == null)
            {
                await _flightRepository.CreateOrUpdateFlight(createdFlight);
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
                    existingFlight.Update(createdFlight);
                    await _flightRepository.CreateOrUpdateFlight(existingFlight);
                }
            }


        }
    }
}
