using Flights.Domain.ValueObjects;

namespace Flights.Domain.Entities
{
    public class Flight : AggregateRoot
    {
        private Flight(DateTime startTime, DateTime endTime, string from, string to)
        {
            FlightId = Guid.NewGuid().ToString();
            StartTime = new StartTime(startTime);
            EndTime = new EndTime(StartTime, endTime);
            From = from;
            To = to;
            Duration = new Duration(StartTime, EndTime);
            Delay = TimeSpan.Zero;
            FlightStarted = false;
            FlightCompleted = false;

        }
        public string FlightId { get; private set; }
        public StartTime StartTime { get; private set; }
        public EndTime EndTime { get; private set; }

        public Duration Duration { get; private set; }

        public TimeSpan Delay { get; private set; }

        public string From { get; private set; }

        public string To { get; private set; }
        public bool FlightStarted { get; private set; }

        public bool FlightCompleted { get; private set; }


        public static Flight Create(DateTime startTime, DateTime endTime, string from, string to)
        {
            
            var flight = new Flight(startTime, endTime, from, to);
            flight.InitializeRoot();

            return flight;
        }
        public void SetFlightStarted()
        {
            FlightStarted = true;
            IncrementVersion();
        }
        public void SetFlightCompleted()
        {

            FlightCompleted = true;
            IncrementVersion() ;

        }
        public void IncrementDelay(TimeSpan delayIncrementedByValue)
        {
            Delay += delayIncrementedByValue;
            IncrementVersion();
        }
        public void DecrementDelay(TimeSpan delayDecrementedByValue)
        {
            if (Delay - delayDecrementedByValue <= TimeSpan.Zero)
            {
                Delay = TimeSpan.Zero;
                IncrementVersion();
                return;
            }
            Delay -= delayDecrementedByValue;
            IncrementVersion();
        }

    }


}
