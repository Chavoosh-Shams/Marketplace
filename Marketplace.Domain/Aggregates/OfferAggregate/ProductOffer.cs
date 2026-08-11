using Marketplace.Domain.Frameworks.Abstracts;
using Marketplace.Domain.Aggregates.OrderAggregates;
using Marketplace.Domain.Aggregates.ProductAggregates;
using Marketplace.Domain.Aggregates.PersonAggregates;

namespace Marketplace.Domain.Aggregates.OfferAggregate
{
    public class ProductOffer : IDbSetEntity
    {
        //Keys
        public Guid Id { get; set; }
        public Guid GuidKey { get; set; }

        //Fields
        public decimal UnitPrice { get; set; }
        public int Stock { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }

        //Navigation Properties
        public Product Product { get; set; }
        public Guid ProductId { get; set; }
        public Seller Seller { get; set; }
        public Guid SellerId { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
