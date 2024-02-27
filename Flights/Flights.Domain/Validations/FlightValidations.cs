using Flights.Domain.Entities;
using Flights.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flights.Domain.Validations
{
    public static class FlightValidations
    {
        public static void Validate(this Flight flight, DateTime startTime, DateTime endTime, string from, string to)
        {
            //Due to the limitations of Entity Framework Core, I had to give up validation in valuable objects
            if (endTime < startTime)
            {
                throw new DomainException("End of the flight must be later than start");
            }

            if (startTime.AddDays(3) < endTime)
            {
                throw new DomainException("Our plane can`t fly that long");
            }
            if (startTime < DateTime.UtcNow)
            {
                throw new DomainException("Can`t add Flight which already started");
            }
            if (String.Equals(from, to, StringComparison.OrdinalIgnoreCase))
            {
                throw new DomainException("Can`t create flight with the same destination");
            }
        }

        public static void ValidateIncrementDelay(this Flight flight)
        {
            if (flight.FlightCompleted)
            {
                throw new DomainException("Can`t increment of already completed flight");
            }
        }
        public void ValidateDecrementDelay(this Flight flight, TimeSpan delayDecrementedByValue)
        {

        }
    }
}
