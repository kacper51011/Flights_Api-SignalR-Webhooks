using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flights.Domain
{
    public abstract class AggregateRoot
    {
        public DateTime CreatedAt { get; private set; }
        public DateTime ViewedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public int Version { get; private set; }


        public void InitializeRoot()
        { 
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            ViewedAt = DateTime.UtcNow;
            Version = 0;
        }
        public void IncrementVersion ()
        {
            Version++;
            UpdatedAt = DateTime.UtcNow;
            ViewedAt = DateTime.UtcNow;

        }
        public void SetViewedDate()
        {
            ViewedAt = DateTime.UtcNow;
        }


    }
}
