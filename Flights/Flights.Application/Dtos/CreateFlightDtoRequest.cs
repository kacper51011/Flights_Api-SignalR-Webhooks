using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flights.Application.Dtos
{
    public class CreateFlightDtoRequest
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public string From { get; set; }
        public string To { get; set; }
    }
}
