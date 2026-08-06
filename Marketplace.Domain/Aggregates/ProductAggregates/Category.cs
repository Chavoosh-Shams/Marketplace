using Marketplace.Domain.Frameworks.Abstracts;

namespace Marketplace.Domain.Aggregates.ProductAggregates
{
    public class Category : IDbSetEntity
    {
        //Keys
        public Guid Id { get; set; }
        public Guid GuidKey { get; set; }

        //Fields
        public string Title { get; set; }
        public bool IsDeleted { get; set; }

        ////Navigation Properties
        public ICollection<Product> Products { get; set; }
    }
}
