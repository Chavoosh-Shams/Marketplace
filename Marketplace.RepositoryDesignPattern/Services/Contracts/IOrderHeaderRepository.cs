

using Marketplace.Domain.Aggregates.OrderAggregates;

namespace Marketplace.RepositoryDesignPattern.Services.Contracts
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
    }
}
