using Marketplace.Domain.Frameworks.Abstracts;
using Marketplace.Domain.Aggregates.OfferAggregate;

namespace Marketplace.Domain.Aggregates.PersonAggregates
{
    public class Seller : IDbSetEntity
    {
        //Keys
        public Guid Id { get; set; }
        public Guid GuidKey { get; set; }

        //Fields
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StoreName { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsDeleted { get; set; }

        //Navigation Properties
        public ICollection<ProductOffer> ProductOffers { get; set; }
    }
}
