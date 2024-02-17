using Flights.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Flights.Domain.ValueObjects
{
    public class EndTime
    {
        public DateTime Value { get; private set; }

        public EndTime(StartTime startTime, DateTime endTime)
        {
            Validate(startTime, endTime);

            Value = endTime;
        }

        private void Validate(StartTime startTime, DateTime endTime)
        {
            if(endTime < startTime.Value)
            {
                throw new DomainException("End of the flight must be later than start");
            }
        }
    }
}
