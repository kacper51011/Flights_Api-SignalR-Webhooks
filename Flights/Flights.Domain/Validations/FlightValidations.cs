using Flights.Domain.Entities;
using Flights.Domain.Exceptions;

namespace Flights.Domain.Validations
{
    public static class FlightValidations
    {
        //public static void ValidateCreation(this Flight flight, DateTime startTime, DateTime endTime, string from, string to)
        //{
        //    //Due to the limitations of Entity Framework Core, I had to give up validation in valuable objects
        //    if (endTime < startTime)
        //    {
        //        throw new DomainException("End of the flight must be later than start");
        //    }

        //    if (startTime.AddDays(3) < endTime)
        //    {
        //        throw new DomainException("Our plane can`t fly that long");
        //    }
        //    if (startTime < DateTime.UtcNow)
        //    {
        //        throw new DomainException("Can`t add Flight which already started");
        //    }
        //}

        public static void ValidateIncrementDelay(this Flight flight)
        {
            if (flight.FlightCompleted)
            {
                throw new DomainException("Can`t increment of already completed flight");
            }
        }
        public static void ValidateDecrementDelay(this Flight flight, TimeSpan delayDecrementedByValue)
        {
            if (flight.FlightCompleted)
            {
                throw new DomainException("Can`t increment of already completed flight");
            }
            if(flight.Delay - delayDecrementedByValue < TimeSpan.Zero)
            {
                throw new DomainException("Flight delay cannot be less than 0 seconds!");
            }
        }


    }
}
