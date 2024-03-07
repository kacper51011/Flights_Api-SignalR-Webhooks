using FlightsConsumer.Domain.Entities;
using System.Text;

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

            var valueBuilder = new StringBuilder();

            //from string


            valueBuilder.Append(flight.From);

            var charsToTenFrom = 10 - flight.From.Length;
            for (var i = 0; i < charsToTenFrom; i++)
            {
                valueBuilder.Append(" ");
            }

            //to string
            valueBuilder.Append(flight.To);

            var charsToTenTo = 10 - flight.To.Length;
            for (var i = 0; i < charsToTenTo; i++)
            {
                valueBuilder.Append(" ");

            }

            //start/end hours and minutes formatted


            valueBuilder.Append(flight.StartTime.Hour.ToString("00"));
            valueBuilder.Append(flight.StartTime.Minute.ToString("00"));

            valueBuilder.Append(flight.EndTime.Hour.ToString("00"));
            valueBuilder.Append(flight.EndTime.Minute.ToString("00"));


            if (flight.FlightCompleted)
            {
                valueBuilder.Append("Ended  ");
            }
            else if (flight.FlightStarted)
            {
                valueBuilder.Append("Flying ");
            }
            else
            {
                valueBuilder.Append("Wait   ");
            }

            // formatted delay
            valueBuilder.Append(flight.Delay.TotalMinutes.ToString("00"));


            flightDto.FormatedValue = valueBuilder.ToString();

            return flightDto;
        }
    }


}
