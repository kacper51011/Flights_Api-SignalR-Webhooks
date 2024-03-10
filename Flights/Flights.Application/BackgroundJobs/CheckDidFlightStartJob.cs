using Flights.Domain.Interfaces;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flights.Application.BackgroundJobs
{
    public class CheckDidFlightStartJob: IJob
    {
        private readonly IFlightsRepository _flightsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CheckDidFlightStartJob(IFlightsRepository flightsRepository, IUnitOfWork unitOfWork)
        {
            _flightsRepository = flightsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var notStartedLatestFlights = await _flightsRepository.GetLatestNotStartedFlights();
            var currentTime = DateTime.UtcNow;

            foreach (var flight in notStartedLatestFlights)
            {
                if (flight.StartTime < currentTime)
                {
                    flight.SetFlightStarted();

                }
            }
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
