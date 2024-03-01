using FlightsConsumer.Application.Dtos;
using FlightsConsumer.Domain.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace FlightsConsumer.Application.Hubs
{
    public class FlightsHub : Hub
    {
        private readonly IFlightsRepository _flightsRepository;
        public FlightsHub(IFlightsRepository flightsRepository)
        {

            _flightsRepository = flightsRepository;

        }

        public override async Task OnConnectedAsync()
        {
            var flights = await _flightsRepository.GetTenLastFlightsFromToday();
            var flightsDtos = new List<FlightSignalRDto>();

            foreach (var flight in flights)
            {
                // created through extension method because its a bit complex mapping
                var flightDto = flight.ToSignalRDto();
                
                flightsDtos.Add(flightDto);
            }
                await Clients.All.SendAsync("Initialize", flightsDtos);
        }

    }




    }



