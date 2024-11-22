using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Domain.Aggregates.CustomerAggregate.ValueObjects
{
    public class CustomerName : IEquatable<CustomerName>
    {
        private readonly int _maxLen = 100;
        private readonly int _minLen = 3;
        public string FirstName { get; }
        public string LastName { get; }

        protected CustomerName(string? firstName, string? lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("firstName cannot be empty.", nameof(firstName));
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("lastName cannot be empty.", nameof(lastName));
            if (firstName.Length<_minLen || firstName.Length>_maxLen)
                throw new ArgumentException($"valid length for firstname is between {_minLen} and {_maxLen}.", nameof(firstName));
            if (lastName.Length < _minLen || lastName.Length > _maxLen)
                throw new ArgumentException($"valid length for lastName is between {_minLen} and {_maxLen}.", nameof(firstName));

            FirstName = firstName;
            LastName = lastName;
        }
        public static CustomerName Parse(string? firstName, string? lastName) => new CustomerName(firstName , lastName);
        public static CustomerName Parse(CustomerName name)
        {
            if(name == null) throw new ArgumentException("CustomerName cannot be empty.", nameof(name));
            return new CustomerName(name.FirstName, name.LastName);
        }
        public bool Equals(CustomerName? other)
        {
            return other != null && FirstName == other.FirstName && LastName==other.LastName;
        }

        public override bool Equals(object? obj) => Equals(obj as CustomerName);

        public override int GetHashCode() => $"FirstName : {FirstName} - LastName: {LastName}".GetHashCode();

        public override string ToString() => $"FirstName : {FirstName} - LastName: {LastName}";
    }
}
