using Flights.Domain.Entities;
using Flights.Domain.Interfaces;
using Flights.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flights.Application.Commands.CreateFlight
{
    public class CreateFlightCommandHandler : IRequestHandler<CreateFlightCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFlightsRepository _flightsRepository;

        public CreateFlightCommandHandler(IUnitOfWork unitOfWork, IFlightsRepository flightsRepository)
        {
            _unitOfWork = unitOfWork;
            _flightsRepository = flightsRepository;
            
        }
        public async Task<string> Handle(CreateFlightCommand request, CancellationToken cancellationToken)
        {
            //there can be 2 flights to the same place at the same time so i won`t check do flight already exist
            try
            {

                var flight = Flight.Create(request.dto.StartTime, request.dto.EndTime, request.dto.From, request.dto.To);
                await _flightsRepository.AddFlight(flight);
                await _unitOfWork.SaveChangesAsync();

                return flight.FlightId;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
