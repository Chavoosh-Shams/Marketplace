using Marketplace.Domain.Frameworks.Abstracts;
using Marketplace.Domain.Aggregates.OrderAggregates;

namespace Marketplace.Domain.Aggregates.CustomerAggregates
{
    public class Customer : IDbSetEntity
    {
        //Keys
        public Guid Id { get; set; }
        public Guid GuidKey { get; set; }

        //Fields
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public bool IsDeleted { get; set; }

        //Navigation Properties
        public ICollection<OrderHeader> OrderHeaders { get; set; }
    }
}
