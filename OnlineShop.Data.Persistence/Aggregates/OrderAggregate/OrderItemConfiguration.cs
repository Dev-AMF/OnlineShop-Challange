using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Core.Domain.Aggregates.OrderAggregate.Entitites;

namespace OnlineShop.Persistence.Context.Aggregates.OrderAggregate
{
    public class OrderItemEntityConfiguration : IEntityTypeConfiguration<OrderItemEntity>
    {
        public void Configure(EntityTypeBuilder<OrderItemEntity> builder)
        {
            builder.HasKey(oi => oi.Id);
            
            builder.OwnsOne(oi =>  oi.Name, name => name.Property(p => p.Value).IsRequired());


            builder.OwnsOne(oi => oi.UnitPrice, price =>
            {
                price.Property(p => p.Value).HasColumnType("decimal(18,2)").IsRequired();
            });
            builder.OwnsOne(oi => oi.PackagingMethod, method =>
            {
                method.Property(m => m.Value).IsRequired();
            });
            builder.OwnsOne(oi => oi.Quantity, quantity =>
            {
                quantity.Property(q => q.Value).IsRequired();
            });
        }
    }
}