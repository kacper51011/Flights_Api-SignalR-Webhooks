using Flights.Domain.Interfaces;
using Quartz;

namespace Flights.Application.BackgroundJobs
{
    public class RandomDelayChangeJob : IJob
    {
        private readonly IFlightsRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public RandomDelayChangeJob(IFlightsRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {

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
                    } else
                    {
                        
                        flights[randomIndex].DecrementDelay(TimeSpan.FromMinutes(randomMinutes));

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
