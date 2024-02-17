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
        public async Task<string> Handle(CreateFlightCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
