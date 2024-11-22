using FluentAssertions;
using OnlineShop.Core.Domain.Aggregates.CustomerAggregate.Entities;
using OnlineShop.Core.Domain.Aggregates.CustomerAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Domain.Test.ProductAggregate.Entitites
{
    public class CustomerEntityTests
    {
        [Fact]
        public void Constructor_WithValidParameters_ShouldCreateCustomer()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = CustomerName.Parse("Alice", "Johnson");

            // Act
            var customer = new CustomerEntity(id, name);

            // Assert
            customer.Id.Should().Be(id);
            customer.Name.Should().Be(name);
        }

        [Fact]
        public void Constructor_WithEmptyId_ShouldThrowArgumentException()
        {
            // Arrange
            var id = Guid.Empty;
            var name = CustomerName.Parse("Alice", "Johnson");

            // Act
            Action act = () => new CustomerEntity(id, name);

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("Id cannot be empty.*");
        }

        [Fact]
        public void Constructor_WithNullName_ShouldThrowArgumentNullException()
        {
            // Arrange
            var id = Guid.NewGuid();
            CustomerName name = null!;

            // Act
            Action act = () => new CustomerEntity(id, name);

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("*name*");
        }

        [Fact]
        public void UpdateName_WithValidName_ShouldUpdateCustomerName()
        {
            // Arrange
            var customer = new CustomerEntity(Guid.NewGuid(), CustomerName.Parse("Alice", "Johnson"));
            var newName = CustomerName.Parse("Alicia", "Johnson");

            // Act
            customer.UpdateName(newName);

            // Assert
            customer.Name.Should().Be(newName);
        }

        [Fact]
        public void UpdateName_WithNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var customer = new CustomerEntity(Guid.NewGuid(), CustomerName.Parse("Alice", "Johnson"));
            CustomerName newName = null!;

            // Act
            Action act = () => customer.UpdateName(newName);

            // Assert
            act.Should().Throw<ArgumentException>();
        }
    }
}
