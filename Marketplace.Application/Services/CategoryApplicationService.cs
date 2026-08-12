using System.Net;
using ResponseFramework;
using Marketplace.Application.Dtos;
using InvoiceApp.Frameworks.ResponseFrameworks;
using Marketplace.Application.Services.Contracts;
using Marketplace.Domain.Aggregates.ProductAggregates;
using InvoiceApp.Frameworks.ResponseFrameworks.Contracts;
using Marketplace.RepositoryDesignPattern.Services.Contracts;

namespace Marketplace.Application.Services
{
    public class CategoryApplicationService : ICategoryApplicationService
    {
        #region [- PrivateField -]
        private readonly ICategoryRepository _categoryRepository;
        #endregion

        #region [- Ctor -]
        public CategoryApplicationService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        #endregion

        #region [- PostAsync() -]
        public async Task<IResponse<PostCategoryDto>> PostAsync(PostCategoryDto postCategoryDto)
        {
            if (postCategoryDto == null)
            {
                return new Response<PostCategoryDto>(
                    false,
                    HttpStatusCode.BadRequest,
                    ResponseMessages.Error,
                    null
                    );
            }
            else
            {
                var category = new Category()
                {
                    GuidKey = postCategoryDto.GuidKey,
                    Title = postCategoryDto.Title
                };
                var result = await _categoryRepository.InsertAsync(category);
                if (!result.IsSuccessful)
                {
                    return new Response<PostCategoryDto>(
                        false,
                        HttpStatusCode.InternalServerError,
                        ResponseMessages.Error,
                        null);
                }
                else
                {
                    return new Response<PostCategoryDto>(
                       true,
                       HttpStatusCode.Created,
                       ResponseMessages.SuccessfullOperation,
                       postCategoryDto);
                }
            }
        }
        #endregion

        #region [- PutAsync() -]
        public async Task<IResponse<PutCategoryDto>> PutAsync(PutCategoryDto putCategoryDto)
        {
            if (putCategoryDto == null)
            {
                return new Response<PutCategoryDto>(
                    false,
                    HttpStatusCode.BadRequest,
                    ResponseMessages.Error,
                    null
                    );
            }
            else
            {
                var category = new Category()
                {
                    GuidKey = putCategoryDto.GuidKey,
                    Title = putCategoryDto.Title
                };
                var result = await _categoryRepository.UpdateAsync(category);
                if (!result.IsSuccessful)
                {
                    return new Response<PutCategoryDto>(
                        false,
                        HttpStatusCode.InternalServerError,
                        ResponseMessages.Error,
                        null);
                }
                else
                {
                    return new Response<PutCategoryDto>(
                       true,
                       HttpStatusCode.OK,
                       ResponseMessages.SuccessfullOperation,
                       putCategoryDto);
                }
            }
        }
        #endregion

        #region [- DeleteAsync() -]
        public async Task<IResponse<DeleteCategoryDto>> DeleteAsync(DeleteCategoryDto deleteCategoryDto)
        {
            if (deleteCategoryDto == null)
            {
                return new Response<DeleteCategoryDto>(
                    false,
                    HttpStatusCode.BadRequest,
                    ResponseMessages.Error,
                    null
                    );
            }
            else
            {
                var category = new Category()
                {
                    GuidKey = deleteCategoryDto.GuidKey,
                    Title = deleteCategoryDto.Title
                };
                var result = await _categoryRepository.DeleteAsync(category);
                if (!result.IsSuccessful)
                {
                    return new Response<DeleteCategoryDto>(
                        false,
                        HttpStatusCode.NotFound,
                        ResponseMessages.NullInput,
                        null);
                }
                else
                {
                    return new Response<DeleteCategoryDto>(
                       true,
                       HttpStatusCode.OK,
                       ResponseMessages.SuccessfullOperation,
                       deleteCategoryDto
                       );
                }
            }
        } 
        #endregion

        #region [- GetByIdAsync() -]
        public async Task<IResponse<GetByIdCategoryDto>> GetByIdAsync(GetByIdCategoryDto getByIdCategoryDto)
        {
            if (getByIdCategoryDto == null)
            {
                return new Response<GetByIdCategoryDto>(
                    false,
                    HttpStatusCode.BadRequest,
                    ResponseMessages.Error,
                    null
                    );
            }
            else
            {
                var category = new Category()
                {
                    GuidKey = getByIdCategoryDto.GuidKey,
                    Title = getByIdCategoryDto.Title
                };
                var categoryDto = await _categoryRepository.SelectByIdAsync(category);
                if (!categoryDto.IsSuccessful || categoryDto.Value == null)
                {
                    return new Response<GetByIdCategoryDto>(
                        false,
                        HttpStatusCode.NotFound,
                        ResponseMessages.NullInput,
                        null);
                }
                else
                {
                    var responseDto = new GetByIdCategoryDto()
                    {
                        GuidKey = categoryDto.Value.GuidKey,
                        Title = categoryDto.Value.Title
                    };
                    return new Response<GetByIdCategoryDto>(
                       true,
                       HttpStatusCode.OK,
                       ResponseMessages.SuccessfullOperation,
                       responseDto);
                }
            }
        }
        #endregion

        #region [- GetAllAsync() -]
        public async Task<IResponse<List<GetAllCategoryDto>>> GetAllAsync()
        {
            var categories = await _categoryRepository.SelectAllAsync();
            if (!categories.IsSuccessful || categories.Value == null)
            {
                return new Response<List<GetAllCategoryDto>>(
                    false,
                    HttpStatusCode.NotFound,
                    ResponseMessages.NullInput,
                    null);
            }
            else
            {
                var result = categories.Value.Select(category => new GetAllCategoryDto()
                {
                    Id = category.Id,
                    GuidKey = category.GuidKey,
                    Title = category.Title
                }).ToList();
                return new Response<List<GetAllCategoryDto>>(
                    true,
                    HttpStatusCode.OK,
                    ResponseMessages.SuccessfullOperation,
                    result);
            }
        }
        #endregion

    }
}
