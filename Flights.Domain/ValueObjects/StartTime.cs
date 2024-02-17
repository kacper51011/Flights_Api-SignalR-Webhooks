using Flights.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flights.Domain.ValueObjects
{
    public class StartTime
    {
        public DateTime Value { get; private set; }

        public StartTime(DateTime startTime)
        {
            Validate(startTime);
            Value = startTime;
        }

        private void Validate(DateTime startTime)
        {
            if(startTime < DateTime.UtcNow)
            {
                throw new DomainException("Can`t add Flight which already started");
            }
        }
    }
}
