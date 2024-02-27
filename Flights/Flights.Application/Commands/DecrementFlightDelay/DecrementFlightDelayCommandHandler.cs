using Flights.Application.Exceptions;
using Flights.Domain.Interfaces;
using Flights.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flights.Application.Commands.DecrementFlightDelay
{
    public class DecrementFlightDelayCommandHandler : IRequestHandler<DecrementFlightDelayCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFlightsRepository _flightsRepository;
        public DecrementFlightDelayCommandHandler(IUnitOfWork unitOfWork, IFlightsRepository flightsRepository)
        {
            _unitOfWork = unitOfWork;
            _flightsRepository = flightsRepository;
        }
        public async Task Handle(DecrementFlightDelayCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var flight = await _flightsRepository.GetFlightById(request.id);
                if (flight == null)
                {
                    throw new NotFoundException("Couldn`t find flight with specified id");
                }

                flight.DecrementDelay(request.decrementDelayValue);
                await _unitOfWork.SaveChangesAsync();

                return;
            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}
