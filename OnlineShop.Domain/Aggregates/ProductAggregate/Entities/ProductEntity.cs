using OnlineShop.Core.Domain.Aggregates.ProductAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Domain.Aggregates.ProductAggregate.Entities
{
    public class ProductEntity
    {
        public Guid Id { get; private set; }
        public ProductName? Name { get; private set; }
        public ProductPackingMethod? PackagingMethod { get; private set; }
        public ProductPrice? Price { get; private set; }

        public ProductEntity(Guid id, ProductName? name, ProductPackingMethod? packagingMethod, ProductPrice? price)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id cannot be empty.", nameof(id));

            Id = id;
            Name = ProductName.Parse(name);
            PackagingMethod = ProductPackingMethod.Parse(packagingMethod);
            Price = ProductPrice.Parse(price);
        }
        protected ProductEntity()
        {
                
        }
        public void UpdateName(ProductName name)
        {
            this.Name = ProductName.Parse(name);
        }
        public void UpdatePackagingMethod(ProductPackingMethod packagingMethod)
        {
            this.PackagingMethod = ProductPackingMethod.Parse(packagingMethod);
        }
        public void UpdatePrice(ProductPrice price) { this.Price = ProductPrice.Parse(price); }
    }
}
