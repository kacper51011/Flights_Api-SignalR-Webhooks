using Flights.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Flights.Application.BackgroundJobs
{
    [DisallowConcurrentExecution]
    public class CheckIsCompletedJob : IJob
    {
        private readonly IFlightsRepository _flightsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CheckIsCompletedJob> _logger;

        public CheckIsCompletedJob(IFlightsRepository flightsRepository, IUnitOfWork unitOfWork, ILogger<CheckIsCompletedJob> logger)
        {
            _flightsRepository = flightsRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                _logger.LogInformation("started checking completion of flights");
                var startedFlights = await _flightsRepository.GetStartedFlights();
                var currentTime = DateTime.UtcNow;

                foreach (var flight in startedFlights)
                {
                    if(flight.EndTime.AddMinutes(flight.Delay.TotalMinutes) < currentTime)
                    {
                        flight.SetFlightCompleted();
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
