using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flights.Application.Commands.CancelSubscription
{
    public record CancelSubscriptionCommand(string secret): IRequest
    {
    }
}
