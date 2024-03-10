using Flights.Domain.Interfaces;
using Quartz;

namespace Flights.Application.BackgroundJobs
{
    public class CheckIsCompletedJob : IJob
    {
        private readonly IFlightsRepository _flightsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CheckIsCompletedJob(IFlightsRepository flightsRepository, IUnitOfWork unitOfWork)
        {
            _flightsRepository = flightsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
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
