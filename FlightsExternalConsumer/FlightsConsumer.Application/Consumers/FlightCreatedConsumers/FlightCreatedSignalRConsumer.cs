using FlightsConsumer.Application.Dtos;
using FlightsConsumer.Application.Hubs;
using FlightsConsumer.Application.Messages;
using FlightsConsumer.Domain.Entities;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsConsumer.Application.Consumers.FlightCreatedConsumers
{
    public class FlightCreatedSignalRConsumer : IConsumer<FlightCreated>
    {
        private readonly IHubContext<FlightsHub> _hubContext;

        public FlightCreatedSignalRConsumer(IHubContext<FlightsHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task Consume(ConsumeContext<FlightCreated> context)
        {
            try
            {
                //we want to show only informations from the same day, 
                if (context.Message.StartTime.Date == DateTime.Today.ToUniversalTime())
                {
                    var createdFlight = Flight.Create(context.Message.FlightId, context.Message.StartTime, context.Message.EndTime, context.Message.Duration, context.Message.Delay, context.Message.From, context.Message.To, context.Message.FlightStarted, context.Message.FlightCompleted, context.Message.DateOfUpdate);
                    var flightDto = createdFlight.ToSignalRDto();
                    await _hubContext.Clients.All.SendAsync("flightcreated", flightDto);
                } else
                {
                    return;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
