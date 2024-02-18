using Flights.Domain.Entities;
using Flights.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flights.Infrastructure.Db
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<Flight> Flights { get; set; }
        public DbSet<WebhookSubscription> WebhookSubscriptions { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            var dbCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
            if (dbCreator != null)
            {
                if (!dbCreator.CanConnect()) dbCreator.Create();
                if (!dbCreator.HasTables()) dbCreator.CreateTables();
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
