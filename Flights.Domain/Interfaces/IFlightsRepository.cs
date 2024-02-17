using Flights.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flights.Domain.Interfaces
{
    public interface IFlightsRepository
    {
        public Task<Flight> GetFlightById(string flightId);
        public Task<List<Flight>> GetCompletedFlights();
        public Task<List<Flight>> GetStartedFlights();
        public Task<List<Flight>> GetAllFlights();

        public Task AddFlight(Flight flight);
        public Task DeleteFlight(Flight flight);


    }
}
