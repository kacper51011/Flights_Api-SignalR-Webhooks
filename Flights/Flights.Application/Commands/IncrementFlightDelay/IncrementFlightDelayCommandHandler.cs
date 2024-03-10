using Flights.Application.Exceptions;
using Flights.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flights.Application.Commands.IncrementFlightDelay
{
    public class IncrementFlightDelayCommandHandler : IRequestHandler<IncrementFlightDelayCommand>
    {
        private readonly IFlightsRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public IncrementFlightDelayCommandHandler(IUnitOfWork unitOfWork, IFlightsRepository repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task Handle(IncrementFlightDelayCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var flight = await _repository.GetFlightById(request.id);
                if(flight == null)
                {
                    throw new NotFoundException("Couldn`t find flight with specified id");
                }
                flight.IncrementDelay(request.incrementDelayValue);

                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
