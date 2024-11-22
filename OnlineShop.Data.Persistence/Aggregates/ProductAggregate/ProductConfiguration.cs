using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Core.Domain.Aggregates.ProductAggregate.Entities;

namespace OnlineShop.Persistence.Context.Aggregates.ProductAggregate
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.HasKey(p => p.Id);
            builder.OwnsOne(p => p.Price, price =>
            {
                price.Property(p => p.Value).HasColumnType("decimal(18,2)").IsRequired();
            });
            builder.OwnsOne(p => p.PackagingMethod, method =>
            {
                method.Property(m => m.Value).IsRequired();
            });
            builder.OwnsOne(p => p.Name, name =>
            {
                name.Property(n => n.Value).HasMaxLength(100).IsRequired();
            });
        }
    }
}