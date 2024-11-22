using FluentAssertions;
using OnlineShop.Core.Domain.Aggregates.ProductAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Domain.Test.ProductAggregate.ValueObjects
{
    public class ProductNameTests
    {
        [Fact]
        public void Parse_WithValidName_ShouldCreateProductName()
        {
            // Arrange
            var name = "Laptop";

            // Act
            var productName = ProductName.Parse(name);

            // Assert
            productName.Value.Should().Be(name);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Parse_WithInvalidName_ShouldThrowArgumentException(string name)
        {
            // Act
            Action act = () => ProductName.Parse(name);

            // Assert
            act.Should().Throw<ArgumentException>();
        }
    }
}
