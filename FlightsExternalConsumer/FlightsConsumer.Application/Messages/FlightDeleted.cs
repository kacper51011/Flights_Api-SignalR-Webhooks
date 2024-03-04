using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsConsumer.Application.Messages
{
    public record FlightDeleted
    {
        public string FlightId { get; set; }
    }
}
