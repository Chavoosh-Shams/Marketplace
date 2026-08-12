using System.Net;
using ResponseFramework;
using InvoiceApp.Frameworks.ResponseFrameworks;
using Marketplace.Domain.Aggregates.ProductAggregates;
using InvoiceApp.ApplicationServices.Dtos.ProductDtos;
using InvoiceApp.ApplicationServices.Services.Contracts;
using InvoiceApp.Frameworks.ResponseFrameworks.Contracts;
using Marketplace.RepositoryDesignPattern.Services.Contracts;

namespace InvoiceApp.ApplicationServices.Services
{
    public class ProductApplicationService : IProductApplicationService
    {
        #region [- PrivateFields -]
        private readonly IProductRepository _productRepository;
        #endregion

        #region [- Ctor -]
        public ProductApplicationService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        #endregion

        #region [- PostAsync() -]
        public async Task<IResponse<PostProductDto>> PostAsync(PostProductDto postProductDto)
        {
            if (postProductDto == null)
            {
                return new Response<PostProductDto>(
                    false,
                    HttpStatusCode.BadRequest,
                    ResponseMessages.Error,
                    null
                    );
            }
            else
            {
                var product = new Product()
                {
                    GuidKey = postProductDto.GuidKey,
                    CategoryId = postProductDto.CateoryId,
                    Title = postProductDto.Title,
                    DescriptionRecord = postProductDto.DescriptionRecord
                };
                var result = await _productRepository.InsertAsync(product);
                if (!result.IsSuccessful)
                {
                    return new Response<PostProductDto>(
                        false,
                        HttpStatusCode.InternalServerError,
                        ResponseMessages.Error,
                        null);
                }
                else
                {
                    return new Response<PostProductDto>(
                       true,
                       HttpStatusCode.Created,
                       ResponseMessages.SuccessfullOperation,
                       postProductDto);
                }
            }
        }
        #endregion

        #region [- PutAsync() -]
        public async Task<IResponse<PutProductDto>> PutAsync(PutProductDto putProductDto)
        {
            if (putProductDto == null)
            {
                return new Response<PutProductDto>(
                    false,
                    HttpStatusCode.BadRequest,
                    ResponseMessages.Error,
                    null
                    );
            }
            else
            {
                var product = new Product()
                {
                    GuidKey = putProductDto.GuidKey,
                    CategoryId = putProductDto.CateoryId,
                    Title = putProductDto.Title,
                    DescriptionRecord = putProductDto.DescriptionRecord
                };
                var result = await _productRepository.UpdateAsync(product);
                if (!result.IsSuccessful)
                {
                    return new Response<PutProductDto>(
                        false,
                        HttpStatusCode.InternalServerError,
                        ResponseMessages.Error,
                        null);
                }
                else
                {
                    return new Response<PutProductDto>(
                       true,
                       HttpStatusCode.OK,
                       ResponseMessages.SuccessfullOperation,
                       putProductDto);
                }
            }
        }
        #endregion

        #region [- DeleteAsync() -]
        public async Task<IResponse<DeleteProductDto>> DeleteAsync(DeleteProductDto deleteProductDto)
        {
            if (deleteProductDto == null)
            {
                return new Response<DeleteProductDto>(
                    false,
                    HttpStatusCode.BadRequest,
                    ResponseMessages.Error,
                    null
                    );
            }
            else
            {
                var product = new Product()
                {
                    GuidKey = deleteProductDto.GuidKey,
                    Title = deleteProductDto.Title,
                    DescriptionRecord = deleteProductDto.DescriptionRecord
                };
                var result = await _productRepository.DeleteAsync(product);
                if (!result.IsSuccessful)
                {
                    return new Response<DeleteProductDto>(
                        false,
                        HttpStatusCode.InternalServerError,
                        ResponseMessages.Error,
                        null);
                }
                else
                {
                    return new Response<DeleteProductDto>(
                       true,
                       HttpStatusCode.OK,
                       ResponseMessages.SuccessfullOperation,
                       deleteProductDto);
                }
            }
        }
        #endregion

        #region [- GetByIdAsync() -]
        public async Task<IResponse<GetProductByIdDto>> GetByIdAsync(GetProductByIdDto getProductByIdDto)
        {
            if (getProductByIdDto == null)
            {
                return new Response<GetProductByIdDto>(
                    false,
                    HttpStatusCode.BadRequest,
                    ResponseMessages.Error,
                    null
                    );
            }
            else
            {
                var product = new Product()
                {
                    GuidKey = getProductByIdDto.GuidKey,
                    CategoryId = getProductByIdDto.CategoryId,
                    Title = getProductByIdDto.Title,
                    DescriptionRecord = getProductByIdDto.DescriptionRecord
                };
                var productDto = await _productRepository.SelectByIdAsync(product);
                if (!productDto.IsSuccessful || productDto.Value == null)
                {
                    return new Response<GetProductByIdDto>(
                        false,
                        HttpStatusCode.NotFound,
                        ResponseMessages.NullInput,
                        null);
                }
                else
                {
                    var responseDto = new GetProductByIdDto()
                    {
                        GuidKey = productDto.Value.GuidKey,
                        CategoryId = productDto.Value.CategoryId,
                        CategoryTitle = productDto.Value.Category.Title,
                        Title = productDto.Value.Title,
                        DescriptionRecord = productDto.Value.DescriptionRecord
                    };
                    return new Response<GetProductByIdDto>(
                        true,
                        HttpStatusCode.OK,
                        ResponseMessages.SuccessfullOperation,
                        responseDto);
                }
            }
        }
        #endregion

        #region [- GetAllAsync() -]
        public async Task<IResponse<List<GetAllProductDto>>> GetAllAsync()
        {
            var products = await _productRepository.SelectAllAsync();
            if (!products.IsSuccessful || products.Value == null)
            {
                return new Response<List<GetAllProductDto>>(
                    false,
                    HttpStatusCode.NotFound,
                    ResponseMessages.NullInput,
                    null);
            }
            else
            {
                var result = products.Value.Select(product => new GetAllProductDto()
                {
                    GuidKey = product.GuidKey,
                    CategoryId = product.CategoryId,
                    CategoryTitle = product.Category.Title,
                    Title = product.Title,
                    DescriptionRecord = product.DescriptionRecord
                }).ToList();
                return new Response<List<GetAllProductDto>>(
                    true,
                    HttpStatusCode.OK,
                    ResponseMessages.SuccessfullOperation,
                    result);
            }
        } 
        #endregion
    }
}