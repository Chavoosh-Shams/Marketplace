using Marketplace.Domain.Aggregates.SellerAggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketplace.EfCore.Configurations
{
    public class SellerConfiguration : IEntityTypeConfiguration<Seller>
    {
        public void Configure(EntityTypeBuilder<Seller> builder)
        {
            builder.ToTable("Seller", "Seller");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(s => s.LastName).IsRequired().HasMaxLength(50);

            builder.Property(c => c.IsDeleted).HasDefaultValue(false).IsRequired();
            builder.HasQueryFilter(c => !c.IsDeleted);
        }
    }
}
