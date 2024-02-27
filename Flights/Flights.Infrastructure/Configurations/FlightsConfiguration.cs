using Flights.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flights.Infrastructure.Configurations
{
    public class FlightsConfiguration : IEntityTypeConfiguration<Flight>
    {
        public void Configure(EntityTypeBuilder<Flight> builder)
        {
            builder.HasKey(f => f.FlightId);

            builder.HasData
                (
                Flight.Create(DateTime.UtcNow.AddHours(2), DateTime.UtcNow.AddHours(4), "Warsaw", "Madrid"),
                Flight.Create(DateTime.UtcNow.AddHours(1.5), DateTime.UtcNow.AddHours(2.5), "Warsaw", "Barcelona"),
                Flight.Create(DateTime.UtcNow.AddHours(3), DateTime.UtcNow.AddHours(5), "Berlin", "Madrid"),
                Flight.Create(DateTime.UtcNow.AddHours(2), DateTime.UtcNow.AddHours(2.5), "Cracov", "Gdansk"),
                Flight.Create(DateTime.UtcNow.AddHours(2), DateTime.UtcNow.AddHours(5), "Warsaw", "Moscow")
                );

        }
    }
}
