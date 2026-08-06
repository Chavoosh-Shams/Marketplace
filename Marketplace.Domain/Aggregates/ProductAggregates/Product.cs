using Marketplace.Domain.Frameworks.Abstracts;
using Marketplace.Domain.Aggregates.OfferAggregate;

namespace Marketplace.Domain.Aggregates.ProductAggregates
{
    public class Product : IDbSetEntity
    {
        //Keys
        public Guid Id { get; set; }
        public Guid GuidKey { get; set; }

        //Fields
        public string Title { get; set; }
        public string DescriptionRecord { get; set; }
        public bool IsDeleted { get; set; }

        //Navigation Properties
        public Category Category { get; set; }
        public Guid CategoryId { get; set; }
        public ICollection<ProductOffer> ProductOffers { get; set; }
    }
}
