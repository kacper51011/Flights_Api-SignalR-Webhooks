using FlightsConsumer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsConsumer.Domain.Interfaces
{
    public interface IFlightsRepository
    {
        public Task<Flight> GetFlightById(string id);
        public Task<List<Flight>> GetAllFlights();
        public Task<List<Flight>> GetCompletedFlights();

        public Task CreateOrUpdateFlight(Flight entity);
        public Task DeleteFlight(string id);

        public Task<List<Flight>> GetTenLastFlightsFromToday();
    }
}
