using Flights.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Flights.Infrastructure.Configurations
{
    public class WebhookSubscriptionsConfiguration : IEntityTypeConfiguration<WebhookSubscription>
    {
        public void Configure(EntityTypeBuilder<WebhookSubscription> builder)
        {
            builder.HasKey(w => w.Id);

            builder.HasData(
                new WebhookSubscription("http://flightsconsumerapi:8005/api/Notifications")
                {
                    Secret = "MyRandomSecret",
                    Id = "RandomlyGeneratedId",
                    WebhookUri = "http://flightsconsumerapi:8005/api/Notifications"
                }
                );
        }
    }
}
