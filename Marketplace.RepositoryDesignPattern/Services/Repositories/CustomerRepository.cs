using System.Net;
using Marketplace.EfCore;
using ResponseFramework;
using Microsoft.EntityFrameworkCore;
using InvoiceApp.Frameworks.ResponseFrameworks;
using InvoiceApp.Frameworks.ResponseFrameworks.Contracts;
using Marketplace.Domain.Aggregates.PersonAggregates;
using Marketplace.RepositoryDesignPattern.Services.Contracts;

namespace Marketplace.RepositoryDesignPattern.Services.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {

        #region [- PrivateField -]
        private readonly MarketplaceDbContext _context;
        #endregion

        #region [- ctor -]
        public CustomerRepository(MarketplaceDbContext context)
        {
            _context = context;
        }
        #endregion

        #region [- InsertAsync() -]
        public async Task<IResponse<Customer>> InsertAsync(Customer customer)
        {
            try
            {
                if (customer == null)
                {
                    return new Response<Customer>(
                        false,
                        HttpStatusCode.BadRequest,
                        ResponseMessages.NullInput,
                        null
                        );
                }
                else
                {
                    await _context.AddAsync(customer);
                    await _context.SaveChangesAsync();
                    return new Response<Customer>(
                        true,
                        HttpStatusCode.Created,
                        ResponseMessages.SuccessfullOperation,
                        customer);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Response<Customer>(
                    false,
                    HttpStatusCode.InternalServerError,
                    ResponseMessages.Error,
                    null
                );
            }
        }
        #endregion

        #region [- UpdateAsync() -]
        public async Task<IResponse<Customer>> UpdateAsync(Customer customer)
        {
            try
            {
                if (customer == null)
                {
                    return new Response<Customer>(
                        false,
                        HttpStatusCode.BadRequest,
                        ResponseMessages.NullInput,
                        null
                        );
                }
                else
                {
                    var existingCustomer = await _context.Set<Customer>().SingleOrDefaultAsync(c => c.GuidKey == customer.GuidKey);
                    if (existingCustomer == null)
                    {
                        return new Response<Customer>(
                            false,
                            HttpStatusCode.NotFound,
                            ResponseMessages.NotFound,
                            null
                            );
                    }
                    else
                    {
                        existingCustomer.FirstName = customer.FirstName;
                        existingCustomer.LastName = customer.LastName;
                        existingCustomer.Phone = customer.Phone;
                        existingCustomer.City = customer.City;
                        existingCustomer.Address = customer.Address;
                        _context.Update(existingCustomer);
                        await _context.SaveChangesAsync();
                        return new Response<Customer>(
                            true,
                            HttpStatusCode.OK,
                            ResponseMessages.SuccessfullOperation,
                            existingCustomer
                            );
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Response<Customer>(
                    false,
                    HttpStatusCode.InternalServerError,
                    ResponseMessages.Error,
                    null
                );
            }
        }
        #endregion

        #region [- DeleteAsync() -]
        public async Task<IResponse<Customer>> DeleteAsync(Customer customer)
        {
            try
            {

                if (customer == null)
                {
                    return new Response<Customer>(
                        false,
                        HttpStatusCode.BadRequest,
                        ResponseMessages.NullInput,
                        null
                        );
                }
                else
                {
                    var existingCustomer = await _context.Set<Customer>().SingleOrDefaultAsync(c => c.GuidKey == customer.GuidKey);
                    if (existingCustomer == null)
                    {
                        return new Response<Customer>(
                            false,
                            HttpStatusCode.NotFound,
                            ResponseMessages.NotFound,
                            null
                            );
                    }
                    else
                    {
                        existingCustomer.IsDeleted = true;
                        _context.Update(existingCustomer);
                        await _context.SaveChangesAsync();
                        return new Response<Customer>(
                            true,
                            HttpStatusCode.OK,
                            ResponseMessages.SuccessfullOperation,
                            existingCustomer
                            );
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Response<Customer>(
                    false,
                    HttpStatusCode.InternalServerError,
                    ResponseMessages.Error,
                    null
                );
            }
        }
        #endregion

        #region [- SelectByIdAsync() -]
        public async Task<IResponse<Customer>> SelectByIdAsync(Customer customer)
        {
            try
            {
                if (customer == null)
                {
                    return new Response<Customer>(
                        false,
                        HttpStatusCode.BadRequest,
                        ResponseMessages.NullInput,
                        null
                        );
                }
                else
                {
                    var existingCustomer = await _context.Set<Customer>().SingleOrDefaultAsync(c => c.GuidKey == customer.GuidKey);
                    if (existingCustomer == null)
                    {
                        return new Response<Customer>(
                            false,
                            HttpStatusCode.NotFound,
                            ResponseMessages.NotFound,
                            null
                            );
                    }
                    else
                    {
                        return new Response<Customer>(
                           true,
                           HttpStatusCode.OK,
                           ResponseMessages.SuccessfullOperation,
                           existingCustomer);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Response<Customer>(
                    false,
                    HttpStatusCode.InternalServerError,
                    ResponseMessages.Error,
                    null
                );
            }
        }
        #endregion

        #region [- SelectAllAsync() -]
        public async Task<IResponse<IEnumerable<Customer>>> SelectAllAsync()
        {
            try
            {
                var customers = await _context.Set<Customer>().AsNoTracking().ToListAsync();
                return new Response<IEnumerable<Customer>>(
                   true,
                   HttpStatusCode.OK,
                   ResponseMessages.SuccessfullOperation,
                   customers);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Response<IEnumerable<Customer>>(
                    false,
                    HttpStatusCode.InternalServerError,
                    ResponseMessages.Error,
                    null
                );
            }
        }
        #endregion

    }
}
