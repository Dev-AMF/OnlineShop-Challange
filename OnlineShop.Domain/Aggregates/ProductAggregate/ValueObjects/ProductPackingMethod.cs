using OnlineShop.Core.Domain.Aggregates.OrderAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Domain.Aggregates.ProductAggregate.ValueObjects
{
    public enum PackingMethods
    {
        Standard,
        Fragile
    }
    public class ProductPackingMethod : IEquatable<ProductPackingMethod>
    {
        
        public PackingMethods Value { get; }

        protected ProductPackingMethod(PackingMethods value)
        {

            Value = value;
        }

        public static ProductPackingMethod Parse(PackingMethods? value)
        {
           if (value == null)
                throw new ArgumentException("ProductPackingMethod cannot be empty.", nameof(value));

            return new ProductPackingMethod((PackingMethods)value);
        }
        public static ProductPackingMethod Parse(ProductPackingMethod? value)
        {
            if (value == null)
                throw new ArgumentException("ProductPackingMethod cannot be empty.", nameof(value));
            return new ProductPackingMethod(value.Value);
        }
        public bool Equals(ProductPackingMethod? other)
        {
            return other != null && Value == other.Value;
        }

        public override bool Equals(object? obj) => Equals(obj as ProductPackingMethod);
        public override int GetHashCode() => Value.GetHashCode();
    }
}
