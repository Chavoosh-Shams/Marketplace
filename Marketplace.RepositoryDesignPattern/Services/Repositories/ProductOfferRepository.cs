using System.Net;
using System.Text.Json;
using ResponseFramework;
using Marketplace.EfCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using InvoiceApp.Frameworks.ResponseFrameworks;
using Marketplace.Domain.Aggregates.OfferAggregate;
using InvoiceApp.Frameworks.ResponseFrameworks.Contracts;
using Marketplace.RepositoryDesignPattern.Services.Contracts;

namespace Marketplace.RepositoryDesignPattern.Services.Repositories
{
    public class ProductOfferRepository : IProductOfferRepository
    {
        #region [- PrivateField -]
        private readonly MarketplaceDbContext _context;
        #endregion

        #region [- ctor -]
        public ProductOfferRepository(MarketplaceDbContext context)
        {
            _context = context;
        }
        #endregion

        #region [- InsertAsync() -]
        public async Task<IResponse<ProductOffer>> InsertAsync(ProductOffer productOffer)
        {
            try
            {
                if (productOffer == null)
                {
                    return new Response<ProductOffer>(
                        false,
                        HttpStatusCode.BadRequest,
                        ResponseMessages.NullInput,
                        null
                        );
                }
                else
                {
                    var jsonString = JsonSerializer.Serialize(productOffer, new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        ReferenceHandler = ReferenceHandler.IgnoreCycles
                    });
                    var jsonParam = new SqlParameter("@JsonData", jsonString);
                    await _context.Database.ExecuteSqlRawAsync(
                        "EXEC dbo.uspInsertProductOffer @JsonData", 
                        jsonParam
                    );
                    return new Response<ProductOffer>(
                        true,
                        HttpStatusCode.Created,
                        ResponseMessages.SuccessfullOperation,
                        productOffer);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Response<ProductOffer>(
                    false,
                    HttpStatusCode.InternalServerError,
                    ResponseMessages.Error,
                    null
                );
            }
        }
        #endregion

        #region [- UpdateAsync() -]
        public async Task<IResponse<ProductOffer>> UpdateAsync(ProductOffer productOffer)
        {
            try
            {
                if (productOffer == null)
                {
                    return new Response<ProductOffer>(
                        false,
                        HttpStatusCode.BadRequest,
                        ResponseMessages.NullInput,
                        null
                        );
                }
                else
                {
                    var jsonString = JsonSerializer.Serialize(productOffer, new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        ReferenceHandler = ReferenceHandler.IgnoreCycles
                    });
                    var jsonParam = new SqlParameter("@JsonData", jsonString);
                    await _context.Database.ExecuteSqlRawAsync(
                        "EXEC dbo.uspUpdateProductOffer @JsonData",
                        jsonParam
                    );
                    return new Response<ProductOffer>(
                        true,
                        HttpStatusCode.OK,
                        ResponseMessages.SuccessfullOperation,
                        productOffer);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Response<ProductOffer>(
                    false,
                    HttpStatusCode.InternalServerError,
                    ResponseMessages.Error,
                    null
                );
            }
        }
        #endregion

        #region [- DeleteAsync() -]
        public async Task<IResponse<ProductOffer>> DeleteAsync(ProductOffer productOffer)
        {
            try
            {
                if (productOffer == null)
                {
                    return new Response<ProductOffer>(
                        false,
                        HttpStatusCode.BadRequest,
                        ResponseMessages.NullInput,
                        null
                        );
                }
                else
                {
                    var jsonString = JsonSerializer.Serialize(productOffer, new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        ReferenceHandler = ReferenceHandler.IgnoreCycles
                    });
                    var jsonParam = new SqlParameter("@JsonData", jsonString);
                    await _context.Database.ExecuteSqlRawAsync(
                        "EXEC dbo.uspDeleteProductOffer @JsonData",
                        jsonParam
                    );
                    return new Response<ProductOffer>(
                        true,
                        HttpStatusCode.OK,
                        ResponseMessages.SuccessfullOperation,
                        productOffer);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Response<ProductOffer>(
                    false,
                    HttpStatusCode.InternalServerError,
                    ResponseMessages.Error,
                    null
                );
            }
        }
        #endregion

        #region [- SelectByIdAsync() -]
        public async Task<IResponse<ProductOffer>> SelectByIdAsync(ProductOffer productOffer)
        {
            try
            {
                if (productOffer == null)
                {
                    return new Response<ProductOffer>(
                        false,
                        HttpStatusCode.BadRequest,
                        ResponseMessages.NullInput,
                        null
                        );
                }
                else
                {
                    var existingProductOffer = await _context.Set<ProductOffer>()
                        .Include(po=>po.Seller)
                        .Include(po=>po.Product)
                        .AsNoTracking()
                        .SingleOrDefaultAsync(po => po.GuidKey == productOffer.GuidKey);
                    if (existingProductOffer == null)
                    {
                        return new Response<ProductOffer>(
                            false,
                            HttpStatusCode.NotFound,
                            ResponseMessages.NotFound,
                            null
                            );
                    }
                    else
                    {
                        return new Response<ProductOffer>(
                            true,
                            HttpStatusCode.OK,
                            ResponseMessages.SuccessfullOperation,
                            existingProductOffer);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Response<ProductOffer>(
                    false,
                    HttpStatusCode.InternalServerError,
                    ResponseMessages.Error,
                    null
                );
            }
        }
        #endregion

        #region [- SelectAllAsync() -]
        public async Task<IResponse<IEnumerable<ProductOffer>>> SelectAllAsync()
        {
            try
            {
                var productOffers = await _context.Set<ProductOffer>()
                   .Include(po => po.Seller)
                   .Include(po => po.Product)
                   .AsNoTracking()
                   .ToListAsync();
                return new Response<IEnumerable<ProductOffer>>(
                    true,
                    HttpStatusCode.OK,
                    ResponseMessages.SuccessfullOperation,
                    productOffers);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Response<IEnumerable<ProductOffer>>(
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
