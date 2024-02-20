using Flights.Application.Messages;
using Flights.Domain.Entities;
using Flights.Domain.Interfaces;
using Flights.Domain.ValueObjects;
using MassTransit;
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
        private readonly IPublishEndpoint _publishEndpoint;

        public CreateFlightCommandHandler( IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
            
        }
        public async Task<string> Handle(CreateFlightCommand request, CancellationToken cancellationToken)
        {
            //there can be 2 flights to the same place at the same time so i won`t check do flight already exist
            try
            {
                //must be used to check if values are correct, also it provides ID later which helps to navigate through flight for update purposes
                var flight = Flight.Create(request.dto.StartTime, request.dto.EndTime, request.dto.From, request.dto.To);

                var message = new FlightCreated()
                {

                };

                await _publishEndpoint.Publish(new FlightCreated { });

                return flight.FlightId;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
