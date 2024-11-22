using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Core.Domain.Aggregates.OrderAggregate.Entitites;

namespace OnlineShop.Persistence.Context.Aggregates.OrderAggregate
{
    public class OrderEntityConfiguration : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            builder.HasKey(o => o.Id);
            builder.HasMany(o => o.OrderItems).WithOne().HasForeignKey("OrderId");
            builder.OwnsOne(o => o.TotalPrice, price =>
            {
                price.Property(p => p.Value).HasColumnType("decimal(18,2)").IsRequired();
            });
            builder.OwnsOne(o => o.ShippingOption, option =>
            {
                option.Property(o => o.Value).IsRequired();
            });
            builder.OwnsOne(o => o.Discount, discount =>
            {
                discount.Property(d => d.DiscountAmount).HasColumnType("decimal(18,2)").IsRequired();
                discount.Property(d => d.DiscountType).IsRequired();
            });
        }
    }
}