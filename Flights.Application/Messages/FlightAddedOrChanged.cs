using Flights.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flights.Application.Messages
{
    public record FlightAddedOrChanged
    {
        public string FlightId { get; set; }

    }
}
