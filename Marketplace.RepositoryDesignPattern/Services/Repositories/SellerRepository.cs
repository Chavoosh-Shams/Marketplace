using System.Net;
using ResponseFramework;
using Marketplace.EfCore;
using Microsoft.EntityFrameworkCore;
using InvoiceApp.Frameworks.ResponseFrameworks;
using Marketplace.Domain.Aggregates.PersonAggregates;
using InvoiceApp.Frameworks.ResponseFrameworks.Contracts;
using Marketplace.RepositoryDesignPattern.Services.Contracts;

namespace Marketplace.RepositoryDesignPattern.Services.Repositories
{
    public class SellerRepository : ISellerRepository
    {
        #region [- PrivateField -]
        private readonly MarketplaceDbContext _context;
        #endregion

        #region [- Ctor -]
        public SellerRepository(MarketplaceDbContext context)
        {
            _context = context;
        }
        #endregion

        #region [- InsertAsync() -]
        public async Task<IResponse<Seller>> InsertAsync(Seller seller)
        {
            try
            {
                if (seller == null)
                {
                    return new Response<Seller>(
                        false,
                        HttpStatusCode.BadRequest,
                        ResponseMessages.NullInput,
                        null
                        );
                }
                else
                {
                    await _context.AddAsync(seller);
                    await _context.SaveChangesAsync();
                    return new Response<Seller>(
                        true,
                        HttpStatusCode.Created,
                        ResponseMessages.SuccessfullOperation,
                        seller);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Response<Seller>(
                    false,
                    HttpStatusCode.InternalServerError,
                    ResponseMessages.Error,
                    null);
            }
        }
        #endregion

        #region [- UpdateAsync() -]
        public async Task<IResponse<Seller>> UpdateAsync(Seller seller)
        {
            try
            {
                if (seller == null)
                {
                    return new Response<Seller>(
                        false,
                        HttpStatusCode.BadRequest,
                        ResponseMessages.NullInput,
                        null
                        );
                }
                else
                {
                    var existingSeller = await _context.Set<Seller>().SingleOrDefaultAsync(s => s.GuidKey == seller.GuidKey);
                    if (existingSeller == null)
                    {
                        return new Response<Seller>(
                            false,
                            HttpStatusCode.NotFound,
                            ResponseMessages.NotFound,
                            null
                            );
                    }
                    else
                    {
                        _context.Entry(existingSeller).CurrentValues.SetValues(seller);
                        await _context.SaveChangesAsync();
                        return new Response<Seller>(
                            true,
                            HttpStatusCode.OK,
                            ResponseMessages.SuccessfullOperation,
                            existingSeller
                            );
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Response<Seller>(
                    false,
                    HttpStatusCode.InternalServerError,
                    ResponseMessages.Error,
                    null);
            }
        }
        #endregion

        #region [- DeleteAsync() -]
        public async Task<IResponse<Seller>> DeleteAsync(Seller seller)
        {
            try
            {
                if (seller == null)
                {
                    return new Response<Seller>(
                        false,
                        HttpStatusCode.BadRequest,
                        ResponseMessages.NullInput,
                        null
                        );
                }
                else
                {
                    var existingSeller = await _context.Set<Seller>().SingleOrDefaultAsync(p => p.GuidKey == seller.GuidKey);
                    if (existingSeller == null)
                    {
                        return new Response<Seller>(
                            false,
                            HttpStatusCode.NotFound,
                            ResponseMessages.NotFound,
                            null
                            );
                    }
                    else
                    {
                        existingSeller.IsDeleted = true;
                        _context.Update(existingSeller);
                        await _context.SaveChangesAsync();
                        return new Response<Seller>(
                            true,
                            HttpStatusCode.OK,
                            ResponseMessages.SuccessfullOperation,
                            existingSeller
                            );
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Response<Seller>(
                    false,
                    HttpStatusCode.InternalServerError,
                    ResponseMessages.Error,
                    null);
            }
        }
        #endregion
       
        #region [- SelectByIdAsync() -]
        public async Task<IResponse<Seller>> SelectByIdAsync(Seller seller)
        {
            try
            {
                if (seller == null)
                {
                    return new Response<Seller>(
                        false,
                        HttpStatusCode.BadRequest,
                        ResponseMessages.NullInput,
                        null
                        );
                }
                else
                {
                    var existingSeller = await _context.Set<Seller>()
                        .SingleOrDefaultAsync(p => p.GuidKey == seller.GuidKey);
                    if (existingSeller == null)
                    {
                        return new Response<Seller>(
                            false,
                            HttpStatusCode.NotFound,
                            ResponseMessages.NotFound,
                            null
                            );
                    }
                    else
                    {
                        return new Response<Seller>(
                            true,
                            HttpStatusCode.OK,
                            ResponseMessages.SuccessfullOperation,
                            existingSeller
                            );
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Response<Seller>(
                    false,
                    HttpStatusCode.InternalServerError,
                    ResponseMessages.Error,
                    null);
            }
        }
        
        #endregion

        #region [- SelectAllAsync() -]
        public async Task<IResponse<IEnumerable<Seller>>> SelectAllAsync()
        {
            try
            {
                var sellers = await _context.Set<Seller>()
                    .ToListAsync();
                return new Response<IEnumerable<Seller>>(
                    true,
                   HttpStatusCode.OK,
                   ResponseMessages.SuccessfullOperation,
                   sellers);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Response<IEnumerable<Seller>>(
                    false,
                    HttpStatusCode.InternalServerError,
                    ResponseMessages.Error,
                    null);
            }
        } 
        #endregion
    }
}
