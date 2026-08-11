using Marketplace.Domain.Aggregates.PersonAggregates;
using Marketplace.Domain.Frameworks.Abstracts;

namespace Marketplace.Domain.Aggregates.OrderAggregates
{
    public class OrderHeader : IDbSetEntity
    {
        //Keys
        public Guid Id { get; set; }
        public Guid GuidKey { get; set; }

        //Fields
        public string ShipCity { get; set; }
        public string ShipAddress { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsDeleted { get; set; }

        //Navigation Properties
        public Customer Customer { get; set; }
        public Guid CustomerId { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
