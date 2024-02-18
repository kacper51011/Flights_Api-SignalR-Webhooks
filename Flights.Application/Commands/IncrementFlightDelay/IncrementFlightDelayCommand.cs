using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flights.Application.Commands.IncrementFlightDelay
{
    public record IncrementFlightDelayCommand(string id, TimeSpan incrementDelayValue) : IRequest
    {
    }
}
