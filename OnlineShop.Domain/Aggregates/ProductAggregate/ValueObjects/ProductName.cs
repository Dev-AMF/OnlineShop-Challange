using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Domain.Aggregates.ProductAggregate.ValueObjects
{
    public class ProductName : IEquatable<ProductName>
    {
        private readonly int _maxLen = 100;
        public string Value { get; }

        protected ProductName(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("ProductName cannot be empty.", nameof(value));
            if (value.Length > _maxLen)
                throw new ArgumentException($"ProductName must be less than {_maxLen} charachters.", nameof(value));

            Value = value;
        }

        public static ProductName Parse(string? value) =>  new ProductName(value);
        public static ProductName Parse(ProductName? value)
        {
            if(value == null) throw new ArgumentNullException(nameof(value));

            return new ProductName(value.Value);
        }
        public bool Equals(ProductName? other)
        {
            return other != null && Value == other.Value;
        }

        public override bool Equals(object? obj) => Equals(obj as ProductName);

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value;
    }
}
