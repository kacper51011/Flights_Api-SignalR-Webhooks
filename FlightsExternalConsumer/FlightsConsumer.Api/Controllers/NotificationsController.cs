using FlightsConsumer.Application.Dtos;
using FlightsConsumer.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightsConsumer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IFlightsRepository _flightsRepository;

        public NotificationsController(IFlightsRepository flightsRepository)
        {
            _flightsRepository = flightsRepository;
        }

        [HttpGet]
        [Route("Test")]
        public async Task<ActionResult<List<FlightSignalRDto>>> testController()
        {
            var flights = await _flightsRepository.GetTenLastFlightsFromToday();
            var flightsDtos = new List<FlightSignalRDto>();

            foreach (var flight in flights)
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

                flightDto.formatedValue = new string(fromStringArray) + new string(toStringArray) + formattedStart + formattedEnd + currentSituation + formattedDelay;

                flightsDtos.Add(flightDto);
            }
            return flightsDtos;
       
    }

    }
}
