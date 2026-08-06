using Marketplace.Domain.Frameworks.Abstracts;
using Marketplace.Domain.Aggregates.OfferAggregate;

namespace Marketplace.Domain.Aggregates.OrderAggregates
{
    public class OrderDetail : IDbSetEntity
    {
        //Keys
        public Guid Id { get; set; }
        public Guid GuidKey { get; set; }

        //Fields
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public bool IsDeleted { get; set; }

        //Navigation Properties
        public OrderHeader OrderHeader { get; set; }
        public Guid OrderHeaderId { get; set; }
        public ProductOffer ProductOffer { get; set; }
        public Guid ProductOfferId { get; set; }
    }
}
