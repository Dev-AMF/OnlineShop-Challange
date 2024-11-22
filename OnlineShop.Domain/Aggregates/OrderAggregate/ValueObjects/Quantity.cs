using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Domain.Aggregates.OrderAggregate.ValueObjects
{
    public class Quantity : IEquatable<Quantity>
    {
        public int Value { get; }

        protected Quantity(int? value)
        {
                if (value == null) throw new ArgumentException("Quantity cannot be empty.", nameof(value));
                if (value <= 0) throw new ArgumentException("Quantity cannot be negetive.", nameof(value));

            Value = Convert.ToInt32(value);
        }
        protected Quantity() { }
        public static Quantity Parse(int? value) => new Quantity(value);
        public bool Equals(Quantity? other)
        {
            return other != null && Value == other.Value;
        }

        public override bool Equals(object? obj) => Equals(obj as Quantity);

        public override int GetHashCode() => Value.GetHashCode();

        public static implicit operator int(Quantity quantity) => quantity.Value;
        public static explicit operator Quantity(int value) => new Quantity(value);
    }
}
