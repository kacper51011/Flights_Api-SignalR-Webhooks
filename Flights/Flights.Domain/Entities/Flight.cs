﻿using Flights.Domain.Exceptions;
using Flights.Domain.Validations;
using System.Runtime.CompilerServices;

namespace Flights.Domain.Entities
{
    public class Flight : AggregateRoot
    {
        private Flight(DateTime startTime, DateTime endTime, string from, string to)
        {
            FlightId = Guid.NewGuid().ToString();
            StartTime = startTime;
            EndTime = endTime;
            From = from;
            To = to;
            Duration = endTime - startTime;
            Delay = TimeSpan.Zero;
            FlightStarted = false;
            FlightCompleted = false;
            IsSendToQueue = false;



        }

        private Flight(DateTime startTime, DateTime endTime, string from, string to, bool isForSeed)
        {
            if(isForSeed == true)
            {
                FlightId = Guid.NewGuid().ToString();
                StartTime = startTime;
                EndTime = endTime;
                From = from;
                To = to;
                Duration = endTime - startTime;
                Delay = TimeSpan.Zero;
                FlightStarted = false;
                FlightCompleted = false;
                IsSendToQueue = false;
            }
            else
            {
                throw new DomainException("Creating object without validation is possible only for seeding");
            }



        }

        private Flight(string id, DateTime startTime, DateTime endTime, string from, string to)
        {

            FlightId = id;
            StartTime = startTime;
            EndTime = endTime;
            From = from;
            To = to;
            Duration = endTime - startTime;
            Delay = TimeSpan.Zero;
            FlightStarted = false;
            FlightCompleted = false;
            IsSendToQueue = false;

        }
        public string FlightId { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        public TimeSpan Duration { get; private set; }

        public TimeSpan Delay { get; private set; }

        public string From { get; private set; }

        public string To { get; private set; }
        public bool FlightStarted { get; private set; }

        public bool FlightCompleted { get; private set; }

        public bool IsSendToQueue { get; private set; }

        public static Flight Create(DateTime startTime, DateTime endTime, string from, string to)
        {
            ValidateCreation(startTime, endTime, from, to);
            var flight = new Flight(startTime, endTime, from, to);

            flight.InitializeRoot();

            return flight;
        }

        public static Flight CreateForSeed(DateTime startTime, DateTime endTime, string from, string to)
        {

            var flight = new Flight(startTime, endTime, from, to, true);
            flight.InitializeRoot();

            return flight;
        }

        public static Flight CreateWithInitializedId(string id, DateTime startTime, DateTime endTime, string from, string to)
        {
            var flight = new Flight(id, startTime, endTime, from, to);
            flight.InitializeRoot();

            return flight;
        }
        public void SetFlightStarted()
        {
            FlightStarted = true;
            IncrementVersion();
            IsSendToQueue = false;

        }
        public void SetFlightCompleted()
        {

            FlightCompleted = true;
            IncrementVersion();
            IsSendToQueue = false;


        }
        public void IncrementDelay(TimeSpan delayIncrementedByValue)
        {
            this.ValidateIncrementDelay();
            Delay += delayIncrementedByValue;
            IncrementVersion();
            IsSendToQueue = false;

        }
        public void DecrementDelay(TimeSpan delayDecrementedByValue)
        {
            this.ValidateDecrementDelay(delayDecrementedByValue);
            if (Delay - delayDecrementedByValue <= TimeSpan.Zero)
            {
                Delay = TimeSpan.Zero;
                IncrementVersion();
                IsSendToQueue = false;
                return;
            }
            Delay -= delayDecrementedByValue;
            IncrementVersion();
            IsSendToQueue = false;

        }

        public static void ValidateCreation(DateTime startTime, DateTime endTime, string from, string to)
        {
            if (endTime < startTime)
            {
                throw new DomainException("End of the flight must be later than start");
            }

            if (startTime.AddDays(3) < endTime)
            {
                throw new DomainException("Our plane can`t fly that long");
            }
            if (startTime < DateTime.UtcNow)
            {
                throw new DomainException("Can`t add Flight which already started");
            }
        }
        public void SetSentToQueue()
        {
            IsSendToQueue = true;
        }
    }


}
