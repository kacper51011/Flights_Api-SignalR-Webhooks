using Flights.Domain.Exceptions;

namespace Flights.Domain.ValueObjects
{
    public class Duration
    {
        public TimeSpan Value { get; private set; }
        public Duration(StartTime startTime, EndTime endTime)
        {
            TimeSpan result = Validate(startTime, endTime);
            Value = result;
        }

        private TimeSpan Validate(StartTime startTime, EndTime endTime)
        {
            // I think the more proper place for this validation would be in endTime value object, but ill leave it here
            //if (startTime.Value > endTime.Value)
            //{
            //    throw new DomainException("End of the flight must be later than start");
            //}

            return endTime.Value - startTime.Value;


        }
    }
}
