using FlightsConsumer.Application.Messages;
using FlightsConsumer.Domain.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsConsumer.Application.Jobs
{
    public class DeleteCompletedFlightsJob : IJob
    {
        private readonly IFlightsRepository _flightsRepository;
        private readonly ILogger<DeleteCompletedFlightsJob> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public DeleteCompletedFlightsJob(IFlightsRepository flightsRepository, ILogger<DeleteCompletedFlightsJob> logger, IPublishEndpoint publishEndpoint)
        {
            _flightsRepository = flightsRepository;
            _logger = logger;
            _publishEndpoint = publishEndpoint; 
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var flightsToDelete = await _flightsRepository.GetCompletedFlights();
                foreach (var flight in flightsToDelete)
                {
                    await _publishEndpoint.Publish(new FlightDeleted { FlightId = flight.FlightId });
                    _logger.LogInformation($"Sent message to rabbitMQ - FlightDeleted {flight.FlightId}");
                }
            }
            catch (Exception)
            {

                _logger.LogWarning("Something went wrong in DeleteCompletedFlightsJob in FlightConsumer");
            }

        }
    }
}
