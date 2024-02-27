using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flights.Application.Dtos
{
    public class GetFlightResponseDto
    {
        public string FlightId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public TimeSpan Duration { get; set; }

        public TimeSpan Delay { get; set; }

        public string From { get; set; }

        public string To { get; set; }
        public bool FlightStarted { get; set; }

        public bool FlightCompleted { get; set; }

        public bool IsSendToQueue { get; set; }
    }
}
