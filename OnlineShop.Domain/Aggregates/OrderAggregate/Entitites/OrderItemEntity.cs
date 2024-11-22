using OnlineShop.Core.Domain.Aggregates.OrderAggregate.ValueObjects;
using OnlineShop.Core.Domain.Aggregates.ProductAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Domain.Aggregates.OrderAggregate.Entitites
{
    public class OrderItemEntity
    {

        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; }
        public ProductName Name { get; private set; }
        public ProductPrice UnitPrice { get; private set; }
        public ProductPackingMethod PackagingMethod { get; private set; }
        public Quantity Quantity { get; private set; }
        public OrderItemEntity(Guid id, Guid productId, ProductName? name, ProductPrice? unitPrice, ProductPackingMethod? packagingMethod, Quantity? quantity)
        {
            if (id == null) throw new ArgumentNullException("id");
            if (productId == null) throw new ArgumentNullException("productId");
            if (id.Equals(Guid.Empty)) throw new ArgumentException("Id cannot be empty.");
            if (productId.Equals(Guid.Empty)) throw new ArgumentException("ProductId cannot be empty.");
           

            Id = id;
            Name = ProductName.Parse(name);
            ProductId = productId;
            UnitPrice = ProductPrice.Parse(unitPrice);
            PackagingMethod = ProductPackingMethod.Parse(packagingMethod);
            Quantity = Quantity.Parse(quantity);
        }

        protected OrderItemEntity()
        {
                
        }
    }
}
