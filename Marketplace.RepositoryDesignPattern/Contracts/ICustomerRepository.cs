using InvoiceApp.Frameworks.ResponseFrameworks.Contracts;
using Marketplace.Domain.Aggregates.CustomerAggregates;

namespace Marketplace.RepositoryDesignPattern.Contracts
{
    public interface ICustomerRepository : IRepository<Customer>
    {

    }
}
