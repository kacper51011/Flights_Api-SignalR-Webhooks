using FlightsConsumer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsConsumer.Application.Dtos
{
    public class FlightSignalRDto
    {     
        public string Id {  get; set; }
        public string FormatedValue { get; set; }
    }


    public static class FlightsExtensions
    {
        public static FlightSignalRDto ToSignalRDto(this Flight flight)
        {
            var flightDto = new FlightSignalRDto();
            flightDto.Id = flight.FlightId;

            var fromStringArray = new Char[10];
            var toStringArray = new Char[10];

            for (int i = 0; i < fromStringArray.Length; i++) { fromStringArray[i] = flight.From[i]; }
            for (int i = 0; i < toStringArray.Length; i++) { toStringArray[i] = flight.To[i]; }

            var formattedStart = flight.StartTime.Minute.ToString() + flight.StartTime.Second.ToString();
            var formattedEnd = flight.EndTime.Minute.ToString() + flight.EndTime.Second.ToString();

            string currentSituation;
            if (flight.FlightCompleted)
            {
                currentSituation = "Ended";
            }
            else if (flight.FlightStarted)
            {
                currentSituation = "Started";
            }
            else
            {
                currentSituation = "Wait";
            }

            var formattedDelay = flight.Delay.TotalMinutes;

            flightDto.FormatedValue = new string(fromStringArray) + new string(toStringArray) + formattedStart + formattedEnd + currentSituation + formattedDelay;

            return flightDto;
        }
    }


}
