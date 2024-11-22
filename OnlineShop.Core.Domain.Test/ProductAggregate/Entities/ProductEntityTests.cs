using FluentAssertions;
using OnlineShop.Core.Domain.Aggregates.ProductAggregate.Entities;
using OnlineShop.Core.Domain.Aggregates.ProductAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Domain.Test.ProductAggregate.Entities
{
    public class ProductEntityTests
    {
        [Fact]
        public void Constructor_WithValidParameters_ShouldCreateProduct()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = ProductName.Parse("Laptop");
            var packagingMethod = ProductPackingMethod.Parse(PackingMethods.Standard);
            var price = ProductPrice.Parse(1500000m);

            // Act
            var product = new ProductEntity(id, name, packagingMethod, price);

            // Assert
            product.Id.Should().Be(id);
            product.Name.Should().Be(name);
            product.PackagingMethod.Should().Be(packagingMethod);
            product.Price.Should().Be(price);
        }

      

        [Fact]
        public void Constructor_WithEmptyId_ShouldThrowArgumentException()
        {
            // Arrange
            var id = Guid.Empty;
            var name = ProductName.Parse("Laptop");
            var packagingMethod = ProductPackingMethod.Parse(PackingMethods.Standard);
            var price = ProductPrice.Parse(1500000m);

            // Act
            Action act = () => new ProductEntity(id, name, packagingMethod, price);

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("Id cannot be empty.*");
        }

        [Fact]
        public void UpdateName_WithValidName_ShouldUpdateProductName()
        {
            // Arrange
            var product = new ProductEntity(Guid.NewGuid(), ProductName.Parse("Laptop"), ProductPackingMethod.Parse(PackingMethods.Standard), ProductPrice.Parse(1500000m));
            var newName = ProductName.Parse("Gaming Laptop");

            // Act
            product.UpdateName(newName);

            // Assert
            product.Name.Should().Be(newName);
        }

        [Fact]
        public void UpdateName_WithNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var product = new ProductEntity(Guid.NewGuid(), ProductName.Parse("Laptop"), ProductPackingMethod.Parse(PackingMethods.Standard), ProductPrice.Parse(1500000m));
            ProductName newName = null!;

            // Act
            Action act = () => product.UpdateName(newName);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void UpdatePackagingMethod_WithValidMethod_ShouldUpdatePackagingMethod()
        {
            // Arrange
            var product = new ProductEntity(Guid.NewGuid(), ProductName.Parse("Laptop"), ProductPackingMethod.Parse(PackingMethods.Standard), ProductPrice.Parse(1500000m));
            var newPackagingMethod = ProductPackingMethod.Parse(PackingMethods.Fragile);

            // Act
            product.UpdatePackagingMethod(newPackagingMethod);

            // Assert
            product.PackagingMethod.Should().Be(newPackagingMethod);
        }

        [Fact]
        public void UpdatePackagingMethod_WithNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var product = new ProductEntity(Guid.NewGuid(), ProductName.Parse("Laptop"), ProductPackingMethod.Parse(PackingMethods.Standard), ProductPrice.Parse(1500000m));
            ProductPackingMethod newPackagingMethod = null!;

            // Act
            Action act = () => product.UpdatePackagingMethod(newPackagingMethod);

            // Assert
            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void UpdatePrice_WithValidPrice_ShouldUpdatePrice()
        {
            // Arrange
            var product = new ProductEntity(Guid.NewGuid(), ProductName.Parse("Laptop"), ProductPackingMethod.Parse(PackingMethods.Standard), ProductPrice.Parse(1500000m));
            var newPrice = ProductPrice.Parse(1400000m);

            // Act
            product.UpdatePrice(newPrice);

            // Assert
            product.Price.Should().Be(newPrice);
        }

        [Fact]
        public void UpdatePrice_WithNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var product = new ProductEntity(Guid.NewGuid(), ProductName.Parse("Laptop"), ProductPackingMethod.Parse(PackingMethods.Standard), ProductPrice.Parse(1500000m));
            ProductPrice newPrice = null!;

            // Act
            Action act = () => product.UpdatePrice(newPrice);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }
    }
}
