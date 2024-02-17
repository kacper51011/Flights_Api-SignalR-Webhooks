using Flights.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flights.Application.Commands.CreateFlight
{
    public record CreateFlightCommand(CreateFlightDtoRequest dto): IRequest<string>
    {
    }
}
