using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FlightsConsumer.Domain
{

    public abstract class AggregateRoot
    {
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public int Version { get; private set; }


        public void InitializeRoot()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Version = 0;
        }
        public void IncrementVersion()
        {
            Version++;
            UpdatedAt = DateTime.UtcNow;

        }

    }
}
