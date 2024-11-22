using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Domain.Aggregates.OrderAggregate.ValueObjects
{
    public class OrderPrice : IEquatable<OrderPrice>
    {
      
        public decimal Value { get; }

        protected OrderPrice(decimal? amount)
        {
            if (amount == null)
                throw new ArgumentException("Price cannot be empty.", nameof(amount));
            if (amount < 0)
                throw new ArgumentException("Price cannot be negative.", nameof(amount));

            Value = Convert.ToDecimal(amount);
        }
        protected OrderPrice() { }
        public static OrderPrice Parse(decimal? value) => new OrderPrice(value);
        public static OrderPrice Parse(OrderPrice? value)
        {
            if(value == null) throw new ArgumentNullException(nameof(value));
            return new OrderPrice(value.Value);
        }
       
        public bool Equals(OrderPrice? other)
        {
            return other != null && Value == other.Value;
        }
        public override bool Equals(object? obj) => Equals(obj as OrderPrice);

        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(OrderPrice left, OrderPrice right) => Equals(left, right);

        public static bool operator !=(OrderPrice left, OrderPrice right) => !Equals(left, right);

        public static bool operator <(OrderPrice left, OrderPrice right) => left.Value < right.Value;
        public static bool operator <=(OrderPrice left, OrderPrice right) => left.Value <= right.Value;
        public static bool operator >(OrderPrice left, OrderPrice right) => left.Value > right.Value;
        public static bool operator >=(OrderPrice left, OrderPrice right) => left.Value >= right.Value;
    }
}
