using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Domain.Aggregates.ProductAggregate.ValueObjects
{
    public class ProductPrice:IEquatable<ProductPrice>    
    {
        public decimal Value { get; }

        protected ProductPrice(decimal? amount)
        {
            if (amount == null)
                throw new ArgumentException("Price cannot be empty.", nameof(amount));
            if (amount < 0)
                throw new ArgumentException("Price cannot be negative.", nameof(amount));

            Value = Convert.ToDecimal(amount);
        }
        protected ProductPrice() { }
        public static ProductPrice Parse(decimal? amount) => new ProductPrice(amount);        
        public static ProductPrice Parse(ProductPrice? amount)
        {
            if(amount == null) throw new ArgumentNullException("ProductPrice cannot be negative.",nameof(amount));
            return new ProductPrice(amount.Value);
        }        

        public bool Equals(ProductPrice? other)
        {
            return other != null && Value == other.Value;
        }

        public override bool Equals(object? obj) => Equals(obj as ProductPrice);

        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(ProductPrice left, ProductPrice right) => Equals(left, right);

        public static bool operator !=(ProductPrice left, ProductPrice right) => !Equals(left, right);
    }
}
