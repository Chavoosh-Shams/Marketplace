
using Marketplace.Domain.Aggregates.PersonAggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketplace.EfCore.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer", "Person");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.City).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Phone).IsRequired().HasMaxLength(50);
            builder.Property(c => c.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(c => c.LastName).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Address).IsRequired().HasMaxLength(200);

            builder.Property(c => c.IsDeleted).HasDefaultValue(false).IsRequired();           
            builder.HasQueryFilter(c => !c.IsDeleted);
        }
    }
}
