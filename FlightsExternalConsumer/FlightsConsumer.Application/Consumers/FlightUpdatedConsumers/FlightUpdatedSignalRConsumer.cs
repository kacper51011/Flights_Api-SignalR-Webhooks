using FlightsConsumer.Application.Dtos;
using FlightsConsumer.Application.Hubs;
using FlightsConsumer.Application.Messages;
using FlightsConsumer.Domain.Entities;
using FlightsConsumer.Domain.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsConsumer.Application.Consumers.FlightUpdatedConsumers
{
    public class FlightUpdatedSignalRConsumer : IConsumer<FlightUpdated>
    {
        private readonly IFlightsRepository _flightsRepository;
        private readonly IHubContext<FlightsHub> _hubContext;

        public FlightUpdatedSignalRConsumer(IFlightsRepository flightsRepository, IHubContext<FlightsHub> hubContext)
        {
            _hubContext = hubContext;
            _flightsRepository = flightsRepository;
        }

        public async Task Consume(ConsumeContext<FlightUpdated> context)
        {
            var flights = await _flightsRepository.GetTenLastFlightsFromToday();
            var flightToUpdate = flights.FirstOrDefault(x => x.FlightId == context.Message.FlightId);

            if (flightToUpdate != null)
            {
                var updatedFlight = Flight.Create(context.Message.FlightId, context.Message.StartTime, context.Message.EndTime, context.Message.Duration, context.Message.Delay, context.Message.From, context.Message.To, context.Message.FlightStarted, context.Message.FlightCompleted, context.Message.DateOfUpdate);
                var dtoToSend = updatedFlight.ToSignalRDto();
                await _hubContext.Clients.All.SendAsync("updateflight", dtoToSend);

            }
            else return;

        }
    }
}
