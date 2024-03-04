using FlightsConsumer.Application.Hubs;
using FlightsConsumer.Application.Messages;
using FlightsConsumer.Domain.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace FlightsConsumer.Application.Consumers.FlightDeletedConsumers
{
    public class FlightDeletedDbConsumer : IConsumer<FlightDeleted>
    {
        private readonly IFlightsRepository _flightsRepository;
        public FlightDeletedDbConsumer(IFlightsRepository flightsRepository)
        {
            _flightsRepository = flightsRepository;

        }
        public async Task Consume(ConsumeContext<FlightDeleted> context)
        {
            try
            {
                var flightToDelete = await _flightsRepository.GetFlightById(context.Message.FlightId);

                if (flightToDelete != null)
                {
                    await _flightsRepository.DeleteFlight(flightToDelete.FlightId);
                }
                else
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
