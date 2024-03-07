using Flights.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flights.Application.Queries.GetAllSubscriptions
{
    public record GetAllSubscriptionsQuery: IRequest<List<GetSubscriptionDto>>
    {
    }
}
