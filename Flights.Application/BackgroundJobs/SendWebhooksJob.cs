using Flights.Application.Messages;
using Flights.Domain.Interfaces;
using MassTransit;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flights.Application.BackgroundJobs
{
    public class SendWebhooksJob : IJob
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IFlightsRepository _flightsRepository;
        public SendWebhooksJob(IPublishEndpoint publishEndpoint, IFlightsRepository flightsRepository)
        {
            _publishEndpoint = publishEndpoint;
            _flightsRepository = flightsRepository;
            
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var notSendedFlights = await _flightsRepository.GetNotSendedFlights();

            foreach(var flight in notSendedFlights)
            {
                await _publishEndpoint.Publish(new FlightAddedOrChanged { FlightId = flight.FlightId});
            }
        }
    }
}
