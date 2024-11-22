using OnlineShop.Core.Domain.Aggregates.CustomerAggregate.Entities;
using OnlineShop.Core.Domain.Aggregates.CustomerAggregate.ValueObjects;
using OnlineShop.Core.Domain.Aggregates.OrderAggregate.Entitites;
using OnlineShop.Core.Domain.Aggregates.OrderAggregate.ValueObjects;
using OnlineShop.Core.Domain.Aggregates.ProductAggregate.Entities;
using OnlineShop.Core.Domain.Aggregates.ProductAggregate.ValueObjects;
using OnlineShop.Persistence.Context.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Persistence.Context.DataSeed
{
    public class DataSeeder
    {
        private readonly OnlineShopDbContext _context;

        public DataSeeder(OnlineShopDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            // Ensure database is created
            await _context.Database.EnsureCreatedAsync();

            // Seed Customers
            if (!_context.Customers.Any())
            {
                var customers = new List<CustomerEntity>
                {
                    new CustomerEntity(Guid.NewGuid(), CustomerName.Parse("Alice", "Johnson")),
                    new CustomerEntity(Guid.NewGuid(), CustomerName.Parse("Bob", "Williams")),
                    new CustomerEntity(Guid.NewGuid(), CustomerName.Parse("Charlie", "Brown"))
                };

                await _context.Customers.AddRangeAsync(customers);
            }

            // Seed Products
            if (!_context.Products.Any())
            {
                var products = new List<ProductEntity>
                {
                    new ProductEntity(Guid.NewGuid(), ProductName.Parse("Headphones"), ProductPackingMethod.Parse(PackingMethods.Standard), ProductPrice.Parse(250000m)),
                    new ProductEntity(Guid.NewGuid(), ProductName.Parse("Monitor"), ProductPackingMethod.Parse(PackingMethods.Fragile), ProductPrice.Parse(750000m)),
                    new ProductEntity(Guid.NewGuid(), ProductName.Parse("Keyboard"), ProductPackingMethod.Parse(PackingMethods.Standard), ProductPrice.Parse(150000m))
                };

                await _context.Products.AddRangeAsync(products);
            }

            // Seed Orders
            if (!_context.Orders.Any())
            {
                // Assuming customers and products are already seeded
                var customer = _context.Customers.First();
                var products = _context.Products.ToList();

                var order = new OrderEntity(Guid.NewGuid(), customer.Id, DateTime.Now);

                

                foreach (var product in products)
                {
                    var orderItem = new OrderItemEntity(
                        Guid.NewGuid(),
                        product.Id,
                        product.Name,
                        product.Price,
                        product.PackagingMethod,
                        Quantity.Parse(1)
                    );

                    order.AddItem(orderItem.Id, orderItem.ProductId, orderItem.Name, orderItem.UnitPrice, orderItem.PackagingMethod, orderItem.Quantity);
                }

                await _context.Orders.AddAsync(order);
            }

            // Save changes
            await _context.SaveChangesAsync();
        }
    }
}
