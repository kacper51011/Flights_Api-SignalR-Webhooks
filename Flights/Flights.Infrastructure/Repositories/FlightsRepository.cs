using Flights.Domain.Entities;
using Flights.Domain.Interfaces;
using Flights.Infrastructure.Db;
using Microsoft.EntityFrameworkCore;
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
        public async Task AddFlight(Flight flight)
        {
            await _context.Flights.AddAsync(flight);
        }

        public void DeleteFlight(Flight flight)
        {
            _context.Flights.Remove(flight);
        }

        public async Task<List<Flight>> GetAllFlights()
        {
            var response = await _context.Flights.ToListAsync();
            return response;

        }

        public async Task<List<Flight>> GetCompletedFlights()
        {
            var response = await _context.Flights.Where(x => x.FlightCompleted == true).ToListAsync();
            return response;
        }

        public async Task<Flight> GetFlightById(string flightId)
        {
            var response = await _context.Flights.FirstOrDefaultAsync(x => x.FlightId == flightId);
            return response;
        }

        public async Task<List<Flight>> GetStartedFlights()
        {
            var response = await _context.Flights.Where(x => x.FlightStarted == true && x.FlightCompleted == false).ToListAsync();
            return response;
        }

        public async Task<List<Flight>> GetNotCompletedFlights()
        {
            var response = await _context.Flights.Where(x => x.FlightCompleted == false).ToListAsync();
            return response;
        }
        public async Task<List<Flight>> GetNotSendedFlights()
        {
            var response = await _context.Flights.Where(x => x.IsSendToQueue == false).ToListAsync();
            return response;
        }
    }
}
