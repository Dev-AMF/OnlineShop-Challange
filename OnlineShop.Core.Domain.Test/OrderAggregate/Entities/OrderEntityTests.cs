using FluentAssertions;
using OnlineShop.Core.Domain.Aggregates.OrderAggregate.Entitites;
using OnlineShop.Core.Domain.Aggregates.OrderAggregate.ValueObjects;
using OnlineShop.Core.Domain.Aggregates.ProductAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Domain.Test.OrderAggregate.Entities
{
     public class OrderEntityTests
    {
        [Fact]
        public void Constructor_WithValidParameters_ShouldCreateOrder()
        {
            // Arrange
            var id = Guid.NewGuid();
            var personId = Guid.NewGuid();
            var orderDate = DateTime.UtcNow;

            // Act
            var order = new OrderEntity(id, personId, orderDate);

            // Assert
            order.Id.Should().Be(id);
            order.PersonId.Should().Be(personId);
            order.OrderDate.Should().Be(orderDate);
            order.OrderItems.Should().BeEmpty();
            order.TotalPrice.Value.Should().Be(0m);
            
        }

        [Theory]
        [InlineData("00011000-0000-0000-0000-000000000030", "2023-01-01")]
        [InlineData("00000000-0000-0000-0000-000000000000", "2023-01-01")]
        [InlineData("11111111-1111-1111-1111-111111111111", "0001-01-01T00:00:00")]
        public void Constructor_WithInvalidParameters_ShouldThrowException(string personIdStr, string orderDateStr)
        {
            // Arrange
            var id = Guid.NewGuid();
            var personId = Guid.Parse(personIdStr);
            var orderDate = DateTime.Parse(orderDateStr);

            // Act
            Action act = () => new OrderEntity(id, personId, orderDate);

            // Assert
            if (personId == Guid.Empty)
            {
                act.Should().Throw<ArgumentException>().WithMessage("PersonId cannot be empty.*");
            }
            else if (orderDate == default)
            {
                act.Should().Throw<ArgumentException>().WithMessage("OrderDate must be a valid date.*");
            }
        }

        [Fact]
        public void AddItem_WithValidParameters_ShouldAddOrderItemAndRecalculateTotalPrice()
        {
            // Arrange
            var order = new OrderEntity(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow);
            var productName = ProductName.Parse("Laptop");
            var unitPrice = ProductPrice.Parse(1500000m);
            var packagingMethod = ProductPackingMethod.Parse(PackingMethods.Standard);
            var quantity = Quantity.Parse(2);
            var orderItemId = Guid.NewGuid();

            // Act
            order.AddItem(orderItemId,Guid.NewGuid() ,productName, unitPrice, packagingMethod, quantity);

            // Assert
            order.OrderItems.Should().HaveCount(1);
            var addedItem = order.OrderItems.First();
            addedItem.Id.Should().Be(orderItemId);
            addedItem.Name.Should().Be(productName);
            addedItem.UnitPrice.Should().Be(unitPrice);
            addedItem.PackagingMethod.Should().Be(packagingMethod);
            addedItem.Quantity.Should().Be(quantity);
            order.CalculateTotalPrice().Value.Should().Be(3000000m);
        }

        [Fact]
        public void AddItem_WithNullName_ShouldThrowException()
        {
            // Arrange
            var order = new OrderEntity(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow);
            ProductName productName = null!;
            var unitPrice = ProductPrice.Parse(1500000m);
            var packagingMethod = ProductPackingMethod.Parse(PackingMethods.Standard);
            var quantity = Quantity.Parse(2);
            var orderItemId = Guid.NewGuid();

            // Act
            Action act = () => order.AddItem(orderItemId, Guid.NewGuid(), productName, unitPrice, packagingMethod, quantity);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ApplyDiscount_WithValidDiscount_ShouldSetDiscountAndRecalculateTotalPrice()
        {
            // Arrange
            var order = new OrderEntity(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow);
            order.AddItem(Guid.NewGuid(), Guid.NewGuid(),ProductName.Parse("Laptop"), ProductPrice.Parse(1500000m), ProductPackingMethod.Parse(PackingMethods.Standard), Quantity.Parse(2)); // Total: 3,000,000

            var discount = Discount.Parse("Percentage", 10); // 10% discount

            // Act
            order.AddDiscount(discount);

            // Assert
            discount.Equals(order.Discount);
            order.CalculateTotalPrice().Value.Should().Be(2700000m); // 3,000,000 - 10% = 2,700,000
        }

        [Fact]
        public void ApplyDiscount_WithAmountDiscount_ShouldSetDiscountAndRecalculateTotalPrice()
        {
            // Arrange
            var order = new OrderEntity(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow);
            order.AddItem(Guid.NewGuid(), Guid.NewGuid(), ProductName.Parse("Smartphone"), ProductPrice.Parse(800000m), ProductPackingMethod.Parse(PackingMethods.Fragile), Quantity.Parse(1)); // Total: 800,000

            var discount = Discount.Parse("Amount", 100000m); // 100,000 discount

            // Act
            order.AddDiscount(discount);

            // Assert
            discount.Equals(order.Discount);
            order.CalculateTotalPrice().Value.Should().Be(700000m); // 800,000 - 100,000 = 700,000
        }

        [Fact]
        public void ApplyDiscount_WithExcessiveAmount_ShouldThrowException()
        {
            // Arrange
            var order = new OrderEntity(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow);
            order.AddItem(Guid.NewGuid(), Guid.NewGuid(), ProductName.Parse("Headphones"), ProductPrice.Parse(250000m), ProductPackingMethod.Parse(PackingMethods.Standard), Quantity.Parse(1)); // Total: 250,000

            var discount = Discount.Parse("Amount", 300000m); // 300,000 discount

            // Act
            Action act = () => order.AddDiscount(discount);

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("discount can not be greater than total price!");
        }

        [Fact]
        public void ApplyDiscount_WithNullDiscount_ShouldThrowException()
        {
            // Arrange
            var order = new OrderEntity(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow);
            Discount discount = null!;

            // Act
            Action act = () => order.AddDiscount(discount);

            // Assert
            act.Should().Throw<ArgumentNullException>().WithMessage("*discount*");
        }
    }
}
