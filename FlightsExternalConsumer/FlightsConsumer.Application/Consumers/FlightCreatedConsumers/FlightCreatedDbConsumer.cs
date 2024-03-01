using FlightsConsumer.Application.Messages;
using FlightsConsumer.Domain.Entities;
using FlightsConsumer.Domain.Interfaces;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsConsumer.Application.Consumers.FlightCreatedConsumers
{
    public class FlightCreatedDbConsumer : IConsumer<FlightCreated>
    {
        private readonly IFlightsRepository _flightsRepository;

        public FlightCreatedDbConsumer(IFlightsRepository flightsRepository)
        {
            _flightsRepository = flightsRepository;
        }

        public async Task Consume(ConsumeContext<FlightCreated> context)
        {

            var createdFlight = Flight.Create(context.Message.FlightId, context.Message.StartTime, context.Message.EndTime, context.Message.Duration, context.Message.Delay, context.Message.From, context.Message.To, context.Message.FlightStarted, context.Message.FlightCompleted, context.Message.DateOfUpdate);
            await _flightsRepository.CreateOrUpdateFlight(createdFlight);
        }
    }
}
