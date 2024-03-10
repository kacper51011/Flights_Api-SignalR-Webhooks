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
        public Task<List<Flight>> GetNotSendedFlights();
        public Task<List<Flight>> GetNotCompletedFlights();
        public Task<List<Flight>> GetLatestNotStartedFlights();


        public Task AddFlight(Flight flight);
        public void DeleteFlight(Flight flight);


    }
}
