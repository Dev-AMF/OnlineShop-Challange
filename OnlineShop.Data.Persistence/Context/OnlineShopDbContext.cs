using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Domain.Aggregates.CustomerAggregate.Entities;
using OnlineShop.Core.Domain.Aggregates.OrderAggregate.Entitites;
using OnlineShop.Core.Domain.Aggregates.ProductAggregate.Entities;
using OnlineShop.Persistence.Context.Aggregates.CustomerAggregate;
using OnlineShop.Persistence.Context.Aggregates.OrderAggregate;
using OnlineShop.Persistence.Context.Aggregates.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Persistence.Context.Context
{
    public class OnlineShopDbContext : DbContext
    {
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<OrderItemEntity> OrderItems { get; set; }
        public DbSet<ProductEntity> Products { get; set; }

        public OnlineShopDbContext(DbContextOptions<OnlineShopDbContext> options)
           : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerEntityConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
