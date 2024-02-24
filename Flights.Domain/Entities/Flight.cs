using Flights.Domain.Exceptions;

namespace Flights.Domain.Entities
{
    public class Flight : AggregateRoot
    {
        private Flight(DateTime startTime, DateTime endTime, string from, string to)
        {
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

        public bool IsSendToQueue {  get; private set; }

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
            IncrementVersion() ;
            IsSendToQueue = false;


        }
        public void IncrementDelay(TimeSpan delayIncrementedByValue)
        {
            Delay += delayIncrementedByValue;
            IncrementVersion();
            IsSendToQueue = false;

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
            IsSendToQueue = false;

        }

        public void Validate(DateTime startTime, DateTime endTime, string from, string to)
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

    }


}
