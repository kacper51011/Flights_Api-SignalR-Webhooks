using Flights.Domain.Entities;
using Flights.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Flights.Application.BackgroundJobs
{
    [DisallowConcurrentExecution]
    public class RandomFlightAddJob : IJob
    {
        private readonly IFlightsRepository _flightsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RandomFlightAddJob> _logger;
        private readonly string[] cities = {
            "London",
            "Paris",
            "Berlin",
            "Madrid",
            "Rome",
            "Athens",
            "Amsterdam",
            "Vienna",
            "Prague",
            "Stockholm",
            "Warsaw",
            "Lisbon",
            "Dublin",
            "Budapest",
            "Copenhagen"
        };

        public RandomFlightAddJob(IFlightsRepository flightsRepository, IUnitOfWork unitOfWork, ILogger<RandomFlightAddJob> logger)
        {
            _flightsRepository = flightsRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }


        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                _logger.LogInformation("started adding random flight");
                var randomization = new Random();

                // random from localization
                var fromLocationIndex = randomization.Next(0, cities.Length);

                int toLocationIndex;
                // random to localization
                //ensuring that we won`t chose the same city second time
                do
                {
                    toLocationIndex = randomization.Next(0, cities.Length);
                } while (toLocationIndex == fromLocationIndex);

                // random addDays for start
                var randomDate = randomization.Next(0, 3);

                var startDate = DateTime.UtcNow.AddDays(randomDate);

                // random addHours for end
                var randomDuration = randomization.Next(1, 12);

                var endDate = startDate.AddHours(randomDuration);


                var createdFlight = Flight.Create(startDate, endDate, cities[fromLocationIndex], cities[toLocationIndex]);

                await _flightsRepository.AddFlight(createdFlight);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
