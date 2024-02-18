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

        public async Task Handle(IncrementFlightDelayCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var flight = await _repository.GetFlightById(request.id);
                if(flight == null)
                {
                    throw new FileNotFoundException();
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
