using Flights.Domain.Entities;
using Flights.Domain.Interfaces;
using Flights.Infrastructure.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flights.Infrastructure.Repositories
{
    public class FlightsRepository : IFlightsRepository
    {
        private readonly ApplicationDbContext _context;

        public FlightsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task AddFlight(Flight flight)
        {
            throw new NotImplementedException();
        }

        public Task DeleteFlight(Flight flight)
        {
            throw new NotImplementedException();
        }

        public Task<List<Flight>> GetAllFlights()
        {
            throw new NotImplementedException();
        }

        public Task<List<Flight>> GetCompletedFlights()
        {
            throw new NotImplementedException();
        }

        public Task<Flight> GetFlightById(string flightId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Flight>> GetStartedFlights()
        {
            throw new NotImplementedException();
        }
    }
}
