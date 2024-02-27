using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsConsumer.Domain
{

        public abstract class AggregateRoot
        {
            public string Id { get; private set; }
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
