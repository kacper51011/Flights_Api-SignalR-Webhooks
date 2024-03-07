using FlightsConsumer.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsConsumer.Application.Commands
{
    public record CreateOrUpdateFlightCommand(IncomingWebhookDto dto): IRequest
    {
    }
}
