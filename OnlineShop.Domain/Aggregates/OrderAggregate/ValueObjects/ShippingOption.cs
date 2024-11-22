using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Domain.Aggregates.OrderAggregate.ValueObjects
{
    public enum ShippingOptions
    {
        RegularPost,
        ExpressPost
    }
    public class ShippingOption : IEquatable<ShippingOption>
    {
       
        public ShippingOptions Value { get; }

        protected ShippingOption(ShippingOptions value)
        {
          
            Value = value;
        }



        public static ShippingOption Parse(string? value)
        {
            var regularShipping = ShippingOptions.RegularPost.ToString();
            var expressShipping = ShippingOptions.ExpressPost.ToString();


            if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("ShippingOption cannot be empty.", nameof(value));
            
            if (value != regularShipping & value != expressShipping)
                throw new ArgumentException($"Only {regularShipping} and {expressShipping} are valid values for ShippingOption.", nameof(value));
            
            return value == regularShipping 
                ? new ShippingOption(ShippingOptions.RegularPost) 
                : new ShippingOption(ShippingOptions.ExpressPost);
        }
        public bool Equals(ShippingOption? other)
        {
            return other != null && Value == other.Value;
        }

        public override bool Equals(object? obj) => Equals(obj as ShippingOption);
        public override int GetHashCode() => Value.GetHashCode();
    }
}
