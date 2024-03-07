using FlightsConsumer.Application.Hubs;
using FlightsConsumer.Application.Messages;
using FlightsConsumer.Domain.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsConsumer.Application.Consumers.FlightDeletedConsumers
{
    public class FlightDeletedSignalRConsumer: IConsumer<FlightDeleted>
    {
            
        private readonly IFlightsRepository _flightsRepository;
        private readonly IHubContext<FlightsHub> _hubContext;
        public FlightDeletedSignalRConsumer(IFlightsRepository flightsRepository, IHubContext<FlightsHub> hubContext)
        {
            _flightsRepository = flightsRepository;
            _hubContext = hubContext;

        }
        public async Task Consume(ConsumeContext<FlightDeleted> context)
        {
            try
            {
                var flights = await _flightsRepository.GetTenLastFlightsFromToday();

                var flightToDelete = flights.FirstOrDefault(x => x.FlightId == context.Message.FlightId);

                if (flightToDelete != null)
                {
                    await _hubContext.Clients.All.SendAsync("deleteFlight", context.Message.FlightId);
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

