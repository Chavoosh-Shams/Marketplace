using Microsoft.EntityFrameworkCore;
using Marketplace.Domain.Aggregates.OrderAggregates;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Marketplace.EfCore.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetail", "Order");

            builder.HasKey(od => od.Id);
            builder.HasIndex(od => new { od.OrderHeaderId, od.ProductOfferId }).IsUnique();

            builder.Property(od => od.UnitPrice).IsRequired();
            builder.Property(od => od.Quantity).IsRequired();

            builder.HasOne(od => od.OrderHeader)
                .WithMany(od => od.OrderDetails)
                .HasForeignKey(od => od.OrderHeaderId)
                .IsRequired()
                 .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(od => od.ProductOffer)
                .WithMany(po => po.OrderDetails)
                .HasForeignKey(od => od.ProductOfferId)
                .IsRequired();

            builder.Property(c => c.IsDeleted).HasDefaultValue(false).IsRequired();
            builder.HasQueryFilter(c => !c.IsDeleted);
        }
    }
}
