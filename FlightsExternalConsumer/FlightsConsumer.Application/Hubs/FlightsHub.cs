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
            await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} has joined");

        }

    }
}
