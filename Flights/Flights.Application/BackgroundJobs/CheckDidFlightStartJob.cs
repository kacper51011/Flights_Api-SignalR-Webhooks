using Flights.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Flights.Application.BackgroundJobs
{
    [DisallowConcurrentExecution]
    public class CheckDidFlightStartJob : IJob
    {
        private readonly IFlightsRepository _flightsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CheckDidFlightStartJob> _logger;

        public CheckDidFlightStartJob(IFlightsRepository flightsRepository, IUnitOfWork unitOfWork, ILogger<CheckDidFlightStartJob> logger)
        {
            _flightsRepository = flightsRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                _logger.LogInformation("started checking start of flights");
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
            catch (Exception)
            {

                throw;
            }

        }
    }
}
