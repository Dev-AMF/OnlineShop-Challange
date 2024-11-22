using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Domain.Aggregates.OrderAggregate.ValueObjects
{
    public class OrderingHours
    {
        public TimeSpan Start { get; }
        public TimeSpan End { get; }

        public OrderingHours(TimeSpan start, TimeSpan end)
        {
            if (end <= start) throw new ArgumentException("End time must be after start time.", nameof(end));
            Start = start;
            End = end;
        }

        public bool IsWithinRange(TimeSpan time)
        {
            return time >= Start && time <= End;
        }
    }
}
