using System.Net;
using ResponseFramework;
using Marketplace.EfCore;
using Microsoft.EntityFrameworkCore;
using InvoiceApp.Frameworks.ResponseFrameworks;
using Marketplace.Domain.Aggregates.ProductAggregates;
using InvoiceApp.Frameworks.ResponseFrameworks.Contracts;
using Marketplace.RepositoryDesignPattern.Services.Contracts;


namespace Marketplace.RepositoryDesignPattern.Services.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        #region [- PrivateField -]
        private readonly MarketplaceDbContext _context;
        #endregion

        #region [- ctor -]
        public CategoryRepository(MarketplaceDbContext context)
        {
            _context = context;
        }
        #endregion

        #region [- InsertAsync() -]
        public async Task<IResponse<Category>> InsertAsync(Category category)
        {
            try
            {
                if (category == null)
                {
                    return new Response<Category>(
                        false,
                        HttpStatusCode.BadRequest,
                        ResponseMessages.NullInput,
                        null
                        );
                }
                else
                {
                    await _context.AddAsync(category);
                    await _context.SaveChangesAsync();
                    return new Response<Category>(
                        true,
                        HttpStatusCode.Created,
                        ResponseMessages.SuccessfullOperation,
                        category);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Response<Category>(
                    false,
                    HttpStatusCode.InternalServerError,
                    ResponseMessages.Error,
                    null
                );
            }
        }
        #endregion

        #region [- UpdateAsync() -]
        public async Task<IResponse<Category>> UpdateAsync(Category category)
        {
            try
            {
                if (category == null)
                {
                    return new Response<Category>(
                        false,
                        HttpStatusCode.BadRequest,
                        ResponseMessages.NullInput,
                        null
                        );
                }
                else
                {
                    var existingCategory = await _context.Set<Category>().SingleOrDefaultAsync(c => c.GuidKey == category.GuidKey);
                    if (existingCategory == null)
                    {
                        return new Response<Category>(
                            false,
                            HttpStatusCode.NotFound,
                            ResponseMessages.NotFound,
                            null
                            );
                    }
                    else
                    {
                        existingCategory.Title = category.Title;
                        _context.Update(existingCategory);
                        await _context.SaveChangesAsync();
                        return new Response<Category>(
                            true,
                            HttpStatusCode.OK,
                            ResponseMessages.SuccessfullOperation,
                            existingCategory
                            );
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Response<Category>(
                    false,
                    HttpStatusCode.InternalServerError,
                    ResponseMessages.Error,
                    null
                );
            }
        }
        #endregion

        #region [- DeleteAsync() -]
        public async Task<IResponse<Category>> DeleteAsync(Category category)
        {
            try
            {

                if (category == null)
                {
                    return new Response<Category>(
                        false,
                        HttpStatusCode.BadRequest,
                        ResponseMessages.NullInput,
                        null
                        );
                }
                else
                {
                    var existingCategory = await _context.Set<Category>().SingleOrDefaultAsync(c => c.GuidKey == category.GuidKey);
                    if (existingCategory == null)
                    {
                        return new Response<Category>(
                            false,
                            HttpStatusCode.NotFound,
                            ResponseMessages.NotFound,
                            null
                            );
                    }
                    else
                    {
                        existingCategory.IsDeleted = true;
                        _context.Update(existingCategory);
                        await _context.SaveChangesAsync();
                        return new Response<Category>(
                            true,
                            HttpStatusCode.OK,
                            ResponseMessages.SuccessfullOperation,
                            existingCategory
                            );
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Response<Category>(
                    false,
                    HttpStatusCode.InternalServerError,
                    ResponseMessages.Error,
                    null
                );
            }
        }
        #endregion

        #region [- SelectByIdAsync() -]
        public async Task<IResponse<Category>> SelectByIdAsync(Category category)
        {
            try
            {
                if (category == null)
                {
                    return new Response<Category>(
                        false,
                        HttpStatusCode.BadRequest,
                        ResponseMessages.NullInput,
                        null
                        );
                }
                else
                {
                    var existingCategory = await _context.Set<Category>().SingleOrDefaultAsync(c => c.GuidKey == category.GuidKey);
                    if (existingCategory == null)
                    {
                        return new Response<Category>(
                            false,
                            HttpStatusCode.NotFound,
                            ResponseMessages.NotFound,
                            null
                            );
                    }
                    else
                    {
                        return new Response<Category>(
                           true,
                           HttpStatusCode.OK,
                           ResponseMessages.SuccessfullOperation,
                           existingCategory);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Response<Category>(
                    false,
                    HttpStatusCode.InternalServerError,
                    ResponseMessages.Error,
                    null
                );
            }
        }
        #endregion

        #region [- SelectAllAsync() -]
        public async Task<IResponse<IEnumerable<Category>>> SelectAllAsync()
        {
            try
            {
                var categories = await _context.Set<Category>().AsNoTracking().ToListAsync();
                return new Response<IEnumerable<Category>>(
                   true,
                   HttpStatusCode.OK,
                   ResponseMessages.SuccessfullOperation,
                   categories);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Response<IEnumerable<Category>>(
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
