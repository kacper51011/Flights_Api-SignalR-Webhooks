using Flights.Domain.Entities;
using Flights.Domain.Interfaces;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Flights.Application.BackgroundJobs
{
    public class RandomFlightAddJob : IJob
    {
        private readonly IFlightsRepository _flightsRepository;
        private readonly IUnitOfWork _unitOfWork;
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

        public RandomFlightAddJob(IFlightsRepository flightsRepository, IUnitOfWork unitOfWork)
        {
            _flightsRepository = flightsRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task Execute(IJobExecutionContext context)
        {
            var randomization = new Random();

            // random from localization
            var fromLocationIndex = randomization.Next(0, cities.Length);

            int toLocationIndex;
            //ensuring that we won`t chose the same city second time
            do
            {
                toLocationIndex = randomization.Next(0, cities.Length);
            } while (toLocationIndex == fromLocationIndex);

            // random addDays for start
            var randomDate = randomization.Next(0, 3);

            var startDate = DateTime.UtcNow.AddDays(randomDate);

            // random addHours for end
            var randomDuration= randomization.Next(1, 12);

            var endDate = startDate.AddHours(randomDuration);


            var createdFlight = Flight.Create(startDate, endDate, cities[fromLocationIndex], cities[toLocationIndex] );

            await _flightsRepository.AddFlight(createdFlight);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
