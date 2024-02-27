using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsConsumer.Domain.Entities
{
    public class Flight: AggregateRoot
    {
        private Flight(string id, DateTime startTime, DateTime endTime, string from, string to, TimeSpan duration, TimeSpan delay, bool flightStarted, bool flightCompleted, DateTime updatedInPublisherAt)
        {


            FlightId = id;
            StartTime = startTime;
            EndTime = endTime;
            From = from;
            To = to;
            Duration = duration;
            Delay = delay;
            FlightStarted = flightStarted;
            FlightCompleted = flightCompleted;
            UpdatedInPublisherAt = updatedInPublisherAt;
        }
        public string FlightId { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        public TimeSpan Duration { get; private set; }

        public TimeSpan Delay { get; private set; }

        public string From { get; private set; }

        public string To { get; private set; }
        public bool FlightStarted { get; private set; }

        public bool FlightCompleted { get; private set; }

        public DateTime UpdatedInPublisherAt { get; private set; }


        public static Flight Create(string id, DateTime startTime, DateTime endTime, TimeSpan duration, TimeSpan delay, string from, string to, bool flightStarted, bool flightCompleted, DateTime updatedInPublisherAt)
        {
            var flight = new Flight(id, startTime, endTime, from, to, duration, delay, flightStarted, flightCompleted, updatedInPublisherAt);
            flight.InitializeRoot();
            return flight;

        }

        public void Update(DateTime startTime, DateTime endTime, TimeSpan duration, TimeSpan delay, string from, string to, bool flightStarted, bool flightCompleted, DateTime updatedInPublisherAt)
        {
            StartTime = startTime;
            EndTime = endTime;
            Duration = duration;
            Delay = delay;
            From = from;
            To = to;
            FlightStarted = flightStarted;
            FlightCompleted = flightCompleted;
            UpdatedInPublisherAt = updatedInPublisherAt;
            IncrementVersion();
        }
    }

}
