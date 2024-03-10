using Flights.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Flights.Application.BackgroundJobs
{
    [DisallowConcurrentExecution]
    public class RandomDelayChangeJob : IJob
    {
        private readonly IFlightsRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RandomDelayChangeJob> _logger;
        public RandomDelayChangeJob(IFlightsRepository repository, IUnitOfWork unitOfWork, ILogger<RandomDelayChangeJob> logger)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                _logger.LogInformation("started adding randomized delay of flights");

                var flights = await _repository.GetNotCompletedFlights();

                if (flights.Count != 0)
                {
                    var randomization = new Random();
                    int randomIndex = randomization.Next(0, flights.Count);

                    var randomAction = randomization.Next(1,4);

                    var randomMinutes = randomization.Next(1, 11);

                    if(randomAction == 1 || randomAction == 2) 
                    {

                        flights[randomIndex].IncrementDelay(TimeSpan.FromMinutes(randomMinutes));
                    } else if(randomAction == 3 && flights[randomIndex].Delay.TotalMinutes > randomMinutes)
                    {
                        
                        flights[randomIndex].DecrementDelay(TimeSpan.FromMinutes(randomMinutes));

                    }else
                    {
                        return;
                    }

                    await _unitOfWork.SaveChangesAsync();


                }




            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
