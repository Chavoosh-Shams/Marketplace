using Marketplace.Domain.Aggregates.ProductAggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketplace.EfCore.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category", "Product");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Title).IsRequired().HasMaxLength(100);

            builder.Property(c => c.IsDeleted).HasDefaultValue(false).IsRequired();
            builder.HasQueryFilter(c => !c.IsDeleted);
        }
    }
}
