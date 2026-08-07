using Microsoft.EntityFrameworkCore;
using Marketplace.Domain.Aggregates.OrderAggregates;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketplace.EfCore.Configurations
{
    public class OrderHeaderConfiguration : IEntityTypeConfiguration<OrderHeader>
    {
        public void Configure(EntityTypeBuilder<OrderHeader> builder)
        {
            builder.ToTable("OrderHeader", "Order");

            builder.HasKey(oh => oh.Id);

            builder.Property(oh => oh.OrderDate).IsRequired();
            builder.Property(oh => oh.ShipCity).IsRequired().HasMaxLength(50);
            builder.Property(oh => oh.ShipAddress).IsRequired().HasMaxLength(200);

            builder.Property(oh => oh.IsDeleted).IsRequired();
            builder.HasOne(oh => oh.Customer)
                .WithMany(oh => oh.OrderHeaders)
                .HasForeignKey(oh => oh.CustomerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(c => c.IsDeleted).HasDefaultValue(false).IsRequired();
            builder.HasQueryFilter(c => !c.IsDeleted);
        }
    }
}
