using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Core.Domain.Aggregates.CustomerAggregate.Entities;

namespace OnlineShop.Persistence.Context.Aggregates.CustomerAggregate
{
    public class CustomerEntityConfiguration : IEntityTypeConfiguration<CustomerEntity>
    {
        public void Configure(EntityTypeBuilder<CustomerEntity> builder)
        {
            builder.HasKey(c => c.Id);
            builder.OwnsOne(c => c.Name, name =>
            {
                name.Property(n => n.FirstName).HasMaxLength(100).IsRequired();
                name.Property(n => n.LastName).HasMaxLength(100).IsRequired();
            });
        }
    }
}