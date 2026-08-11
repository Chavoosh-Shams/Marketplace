using InvoiceApp.Frameworks.ResponseFrameworks.Contracts;

namespace Marketplace.RepositoryDesignPattern.Services.Contracts
{
    public interface IRepository<T> where T : class
    {
        Task<IResponse<T>> InsertAsync(T obj);
        Task<IResponse<T>> UpdateAsync(T obj);
        Task<IResponse<T>> DeleteAsync(T obj);
        Task<IResponse<T>> SelectByIdAsync(T obj);
        Task<IResponse<IEnumerable<T>>> SelectAllAsync();
    }
}
