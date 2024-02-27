using Flights.Application.Dtos;
using Flights.Application.Exceptions;
using Flights.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flights.Application.Queries.GetFlightById
{
    public class GetFlightByIdQueryHandler : IRequestHandler<GetFlightByIdQuery, GetFlightResponseDto>
    {
        private readonly IFlightsRepository _flightsRepository;

        public GetFlightByIdQueryHandler(IFlightsRepository flightsRepository)
        {
            _flightsRepository = flightsRepository;
        }

        public async Task<GetFlightResponseDto> Handle(GetFlightByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var flight = await _flightsRepository.GetFlightById(request.id);
                if (flight == null )
                {
                    throw new NotFoundException("Couldn`t find flight with specified id, it doesn`t exists or flight already ended");
                }

                var dto = new GetFlightResponseDto()
                {
                    FlightId = flight.FlightId,
                    To = flight.To,
                    From = flight.From,
                    Duration = flight.Duration,
                    Delay = flight.Delay,
                    FlightCompleted = flight.FlightCompleted,
                    FlightStarted = flight.FlightStarted,
                    StartTime = flight.StartTime,
                    EndTime = flight.EndTime,
                    IsSendToQueue = flight.IsSendToQueue
                };

                return dto;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
