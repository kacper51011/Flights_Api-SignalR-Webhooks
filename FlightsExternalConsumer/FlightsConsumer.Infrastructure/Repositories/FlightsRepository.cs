using FlightsConsumer.Domain.Entities;
using FlightsConsumer.Domain.Interfaces;
using FlightsConsumer.Infrastructure.Db;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsConsumer.Infrastructure.Repositories
{
    public class FlightsRepository : IFlightsRepository
    {
        private IMongoCollection<Flight> _flightsCollection;
        public FlightsRepository(IOptions<MongoSettings> flightsDatabaseSettings)
        {
            var mongoClient = new MongoClient(flightsDatabaseSettings.Value.ConnectionString);

            var database = mongoClient.GetDatabase(flightsDatabaseSettings.Value.DatabaseName);

            _flightsCollection = database.GetCollection<Flight>(flightsDatabaseSettings.Value.FlightsCollectionName);
        }
        public async Task CreateOrUpdateFlight(Flight entity)
        {
            await _flightsCollection.ReplaceOneAsync(e => e.FlightId == entity.FlightId, entity, new ReplaceOptions()
            {
                IsUpsert = true
            });
        }

        public async Task DeleteFlight(string id)
        {
            await _flightsCollection.DeleteOneAsync(e => e.FlightId == id);
        }

        public async Task<List<Flight>> GetAllFlights()
        {
            var emptyFilter = Builders<Flight>.Filter.Empty;
            var response = await _flightsCollection.Find(emptyFilter).ToListAsync();

            return response;
        }

        public async Task<Flight> GetFlightById(string id)
        {
            var response = await _flightsCollection.Find(x => x.FlightId == id).FirstOrDefaultAsync();
            return response;
        }
    }
}
