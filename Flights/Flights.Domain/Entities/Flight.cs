using Flights.Domain.Validations;

namespace Flights.Domain.Entities
{
    public class Flight : AggregateRoot
    {
        private Flight(DateTime startTime, DateTime endTime, string from, string to)
        {
            this.ValidateCreation(startTime, endTime, from, to);
            FlightId = Guid.NewGuid().ToString();
            StartTime = startTime;
            EndTime = endTime;
            From = from;
            To = to;
            Duration = endTime - startTime;
            Delay = TimeSpan.Zero;
            FlightStarted = false;
            FlightCompleted = false;
            IsSendToQueue = false;



        }

        private Flight(string id, DateTime startTime, DateTime endTime, string from, string to)
        {

            this.ValidateCreation(startTime, endTime, from, to);
            FlightId = id;
            StartTime = startTime;
            EndTime = endTime;
            From = from;
            To = to;
            Duration = endTime - startTime;
            Delay = TimeSpan.Zero;
            FlightStarted = false;
            FlightCompleted = false;
            IsSendToQueue = false;

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

        public bool IsSendToQueue { get; private set; }

        public static Flight Create(DateTime startTime, DateTime endTime, string from, string to)
        {

            var flight = new Flight(startTime, endTime, from, to);
            flight.InitializeRoot();

            return flight;
        }

        public static Flight CreateWithInitializedId(string id, DateTime startTime, DateTime endTime, string from, string to)
        {
            var flight = new Flight(id, startTime, endTime, from, to);
            flight.InitializeRoot();

            return flight;
        }
        public void SetFlightStarted()
        {
            FlightStarted = true;
            IncrementVersion();
            IsSendToQueue = false;

        }
        public void SetFlightCompleted()
        {

            FlightCompleted = true;
            IncrementVersion();
            IsSendToQueue = false;


        }
        public void IncrementDelay(TimeSpan delayIncrementedByValue)
        {
            this.ValidateIncrementDelay();
            Delay += delayIncrementedByValue;
            IncrementVersion();
            IsSendToQueue = false;

        }
        public void DecrementDelay(TimeSpan delayDecrementedByValue)
        {
            this.ValidateDecrementDelay(delayDecrementedByValue);
            if (Delay - delayDecrementedByValue <= TimeSpan.Zero)
            {
                Delay = TimeSpan.Zero;
                IncrementVersion();
                return;
            }
            Delay -= delayDecrementedByValue;
            IncrementVersion();
            IsSendToQueue = false;

        }
    }


}
