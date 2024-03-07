using FlightsConsumer.Domain.Entities;

namespace FlightsConsumer.Application.Dtos
{
    public class FlightSignalRDto
    {
        public string Id { get; set; }
        public string FormatedValue { get; set; }
    }


    public static class FlightsExtensions
    {
        public static FlightSignalRDto ToSignalRDto(this Flight flight)
        {
            var flightDto = new FlightSignalRDto();
            flightDto.Id = flight.FlightId;

            // lets say the max string length of from and to is 10 so it will be a bit easier to map

            //from string

            var fromStringArray = new List<string>();

            fromStringArray.Add(flight.From);

            var charsToTenFrom = 10 - flight.From.Length;

            for (var i = 0; i < charsToTenFrom; i++)
            {
                fromStringArray.Add(" ");
            }

            //to string
            var toStringArray = new List<string>();

            toStringArray.Add(flight.To);

            var charsToTenTo = 10 - flight.To.Length;

            for (var i = 0; i < charsToTenTo; i++)
            {
                toStringArray.Add(" ");
            }

            //start and end formatted values

            var formattedStart = flight.StartTime.Hour.ToString("00") + flight.StartTime.Minute.ToString("00");
            var formattedEnd = flight.EndTime.Hour.ToString("00") + flight.EndTime.Minute.ToString("00");

            string currentSituation;
            if (flight.FlightCompleted)
            {
                currentSituation = "Ended  ";
            }
            else if (flight.FlightStarted)
            {
                currentSituation = "Flying ";
            }
            else
            {
                currentSituation = "Wait   ";
            }



            string formattedDelay = flight.Delay.TotalMinutes.ToString("00");


            flightDto.FormatedValue = string.Join("", string.Join("", fromStringArray), string.Join("", toStringArray), formattedStart, formattedEnd, currentSituation, formattedDelay);

            return flightDto;
        }
    }


}
