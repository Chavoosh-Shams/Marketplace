using Microsoft.EntityFrameworkCore;
using Marketplace.Domain.Aggregates.OfferAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Marketplace.EfCore.Configurations
{
    public class ProductOffferConfiguration : IEntityTypeConfiguration<ProductOffer>
    {
        public void Configure(EntityTypeBuilder<ProductOffer> builder)
        {
            builder.ToTable("ProductOffer", "Marketplace");

            builder.HasKey(po => po.Id);
            builder.HasIndex(po => new { po.ProductId, po.SellerId }).IsUnique();

            builder.Property(po => po.UnitPrice).IsRequired();
            builder.Property(po => po.Stock).IsRequired();
            builder.Property(po => po.IsActive).IsRequired();
            builder.Property(po => po.CreatedDate).IsRequired();

            builder.HasOne(po => po.Product)
                .WithMany(p => p.ProductOffers)
                .HasForeignKey(po => po.ProductId)
                .IsRequired();

            builder.HasOne(po => po.Seller)
                .WithMany(s => s.ProductOffers)
                .HasForeignKey(po => po.SellerId)
                .IsRequired();

            builder.Property(c => c.IsDeleted).HasDefaultValue(false).IsRequired();
            builder.HasQueryFilter(c => !c.IsDeleted);
        }
    }
}
