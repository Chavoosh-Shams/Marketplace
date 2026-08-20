using System.Net;
using ResponseFramework;
using InvoiceApp.Frameworks.ResponseFrameworks;
using Marketplace.Application.Services.Contracts;
using Marketplace.Domain.Aggregates.OfferAggregate;
using Marketplace.Application.Dtos.ProductOfferDtos;
using InvoiceApp.Frameworks.ResponseFrameworks.Contracts;
using Marketplace.RepositoryDesignPattern.Services.Contracts;

namespace Marketplace.Application.Services
{
    public class ProductOfferApplicationService : IProductOfferApplicationService
    {
        #region [- PrivateField -]
        private readonly IProductOfferRepository _productOfferRepository;
        #endregion

        #region [- Ctor -]
        public ProductOfferApplicationService(IProductOfferRepository productOfferRepository)
        {
            _productOfferRepository = productOfferRepository;
        }
        #endregion

        #region [- PostAsync() -]
        public async Task<IResponse<PostProductOfferDto>> PostAsync(PostProductOfferDto postProductOfferDto)
        {
            if (postProductOfferDto == null)
            {
                return new Response<PostProductOfferDto>(
                    false,
                    HttpStatusCode.BadRequest,
                    ResponseMessages.Error,
                    null
                    );
            }
            else
            {
                var productOffer = new ProductOffer()
                {
                    GuidKey = postProductOfferDto.GuidKey,
                    ProductId = postProductOfferDto.ProductId,
                    SellerId = postProductOfferDto.SellerId,
                    UnitPrice = postProductOfferDto.UnitPrice,
                    Stock = postProductOfferDto.Stock,
                    IsActive = postProductOfferDto.IsActive,
                    CreatedDate = postProductOfferDto.CreatedDate
                };
                var result = await _productOfferRepository.InsertAsync(productOffer);
                if (!result.IsSuccessful)
                {
                    return new Response<PostProductOfferDto>(
                        false,
                        HttpStatusCode.InternalServerError,
                        ResponseMessages.Error,
                        null);
                }
                else
                {

                    return new Response<PostProductOfferDto>(
                       true,
                       HttpStatusCode.Created,
                       ResponseMessages.SuccessfullOperation,
                       postProductOfferDto);
                }
            }
        }
        #endregion

        #region [- PutAsync() -]
        public async Task<IResponse<PutProductOfferDto>> PutAsync(PutProductOfferDto putProductOfferDto)
        {
            if (putProductOfferDto == null)
            {
                return new Response<PutProductOfferDto>(
                    false,
                    HttpStatusCode.BadRequest,
                    ResponseMessages.Error,
                    null
                    );
            }
            else
            {
                var productOffer = new ProductOffer()
                {
                    GuidKey = putProductOfferDto.GuidKey,
                    ProductId = putProductOfferDto.ProductId,
                    SellerId = putProductOfferDto.SellerId,
                    UnitPrice = putProductOfferDto.UnitPrice,
                    Stock = putProductOfferDto.Stock,
                    IsActive = putProductOfferDto.IsActive,
                    CreatedDate = putProductOfferDto.CreatedDate
                };
                var result = await _productOfferRepository.UpdateAsync(productOffer);
                if (!result.IsSuccessful)
                {
                    return new Response<PutProductOfferDto>(
                        false,
                        HttpStatusCode.InternalServerError,
                        ResponseMessages.Error,
                        null);
                }
                else
                {

                    return new Response<PutProductOfferDto>(
                       true,
                       HttpStatusCode.OK,
                       ResponseMessages.SuccessfullOperation,
                       putProductOfferDto);
                }
            }
        }
        #endregion

        #region [- DeleteAsync() -]
        public async Task<IResponse<DeleteProductOfferDto>> DeleteAsync(DeleteProductOfferDto deleteProductOfferDto)
        {
            if (deleteProductOfferDto == null)
            {
                return new Response<DeleteProductOfferDto>(
                    false,
                    HttpStatusCode.BadRequest,
                    ResponseMessages.Error,
                    null
                    );
            }
            else
            {
                var productOffer = new ProductOffer()
                {
                    GuidKey = deleteProductOfferDto.GuidKey,
                    ProductId = deleteProductOfferDto.ProductId,
                    SellerId = deleteProductOfferDto.SellerId,
                    UnitPrice = deleteProductOfferDto.UnitPrice,
                    Stock = deleteProductOfferDto.Stock,
                    IsActive = deleteProductOfferDto.IsActive,
                    CreatedDate = deleteProductOfferDto.CreatedDate
                };
                var result = await _productOfferRepository.DeleteAsync(productOffer);
                if (!result.IsSuccessful)
                {
                    return new Response<DeleteProductOfferDto>(
                        false,
                        HttpStatusCode.InternalServerError,
                        ResponseMessages.Error,
                        null);
                }
                else
                {

                    return new Response<DeleteProductOfferDto>(
                       true,
                       HttpStatusCode.OK,
                       ResponseMessages.SuccessfullOperation,
                       deleteProductOfferDto);
                }
            }
        }
        #endregion

        #region [- GetByIdAsync() -]
        public async Task<IResponse<GetByIdProductOfferDto>> GetByIdAsync(GetByIdProductOfferDto getByIdProductOfferDto)
        {
            if (getByIdProductOfferDto == null)
            {
                return new Response<GetByIdProductOfferDto>(
                    false,
                    HttpStatusCode.BadRequest,
                    ResponseMessages.Error,
                    null
                    );
            }
            else
            {
                var productOffer = new ProductOffer()
                {
                    GuidKey = getByIdProductOfferDto.GuidKey,
                    ProductId = getByIdProductOfferDto.ProductId,
                    SellerId = getByIdProductOfferDto.SellerId,
                    UnitPrice = getByIdProductOfferDto.UnitPrice,
                    Stock = getByIdProductOfferDto.Stock,
                    IsActive = getByIdProductOfferDto.IsActive,
                    CreatedDate = getByIdProductOfferDto.CreatedDate
                };
                var productOfferDto = await _productOfferRepository.SelectByIdAsync(productOffer);
                if (!productOfferDto.IsSuccessful || productOfferDto.Value == null)
                {
                    return new Response<GetByIdProductOfferDto>(
                        false,
                        HttpStatusCode.NotFound,
                        ResponseMessages.NullInput,
                        null);
                }
                else
                {
                    var responseDto = new GetByIdProductOfferDto()
                    {
                        GuidKey = productOfferDto.Value.GuidKey,
                        ProductId = productOfferDto.Value.ProductId,
                        ProductTitle = productOfferDto.Value.Product.Title,
                        ProductDescriptionRecord = productOfferDto.Value.Product.DescriptionRecord,
                        SellerId = productOfferDto.Value.SellerId,
                        SellerFirstName = productOfferDto.Value.Seller.FirstName,
                        SellerLastName = productOfferDto.Value.Seller.LastName,
                        UnitPrice = productOfferDto.Value.UnitPrice,
                        Stock = productOfferDto.Value.Stock,
                        IsActive = productOfferDto.Value.IsActive,
                        CreatedDate = productOfferDto.Value.CreatedDate
                    };
                    return new Response<GetByIdProductOfferDto>(
                       true,
                       HttpStatusCode.OK,
                       ResponseMessages.SuccessfullOperation,
                       responseDto);
                }
            }
        }
        #endregion

        #region [- GetAllAsync() -]
        public async Task<IResponse<List<GetAllProductOfferDto>>> GetAllAsync()
        {
            var productOffers = await _productOfferRepository.SelectAllAsync();
            if (!productOffers.IsSuccessful || productOffers.Value == null)
            {
                return new Response<List<GetAllProductOfferDto>>(
                    false,
                    HttpStatusCode.NotFound,
                    ResponseMessages.NullInput,
                    null);
            }
            else
            {
                var result = productOffers.Value.Select(productOffer => new GetAllProductOfferDto()
                {
                    Id = productOffer.Id,
                    GuidKey = productOffer.GuidKey,
                    ProductId = productOffer.ProductId,
                    ProductTitle = productOffer.Product.Title,
                    ProductDescriptionRecord = productOffer.Product.DescriptionRecord,
                    SellerId = productOffer.SellerId,
                    SellerFirstName = productOffer.Seller.FirstName,
                    SellerLastName = productOffer.Seller.LastName,
                    UnitPrice = productOffer.UnitPrice,
                    Stock = productOffer.Stock,
                    IsActive = productOffer.IsActive,
                    CreatedDate = productOffer.CreatedDate
                }).ToList();

                return new Response<List<GetAllProductOfferDto>>(
                    true,
                    HttpStatusCode.OK,
                    ResponseMessages.SuccessfullOperation,
                    result);
            }
        }
        #endregion
    }
}