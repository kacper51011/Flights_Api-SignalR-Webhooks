using FlightsConsumer.Application.Messages;
using FlightsConsumer.Domain.Entities;
using FlightsConsumer.Domain.Interfaces;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsConsumer.Application.Consumers.FlightUpdatedConsumers
{
    public class FlightUpdatedDbConsumer : IConsumer<FlightUpdated>
    {
        private readonly IFlightsRepository _flightsRepository;

        public FlightUpdatedDbConsumer(IFlightsRepository flightsRepository)
        {
            _flightsRepository = flightsRepository;
        }

        public async Task Consume(ConsumeContext<FlightUpdated> context)
        {
            try
            {
                var updatedFlightData = Flight.Create(context.Message.FlightId, context.Message.StartTime, context.Message.EndTime, context.Message.Duration, context.Message.Delay, context.Message.From, context.Message.To, context.Message.FlightStarted, context.Message.FlightCompleted, context.Message.DateOfUpdate);
                await _flightsRepository.CreateOrUpdateFlight(updatedFlightData);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
