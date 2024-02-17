using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flights.Domain.Entities
{
    public class Flight: AggregateRoot
    {
        private Flight()
        {
            FlightId = Guid.NewGuid().ToString();

            
        }
        public string FlightId { get; private set; }
        public StartTime StartTime { get; private set; }
        public EndTime EndTime { get; private set;}

        public Duration Duration { get; private set;}

        public TimeSpan Delay { get; private set;}

        public string FlyingFrom { get; private set; }

        public string FlyingTo { get; private set; }
        public bool FlightStarted { get; private set; }

        public bool FlightCompleted { get; private set;}


    }


}
