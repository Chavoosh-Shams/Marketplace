
using InvoiceApp.ApplicationServices.Dtos.ProductDtos;
using Marketplace.Application.Dtos.ProductOfferDtos;

namespace Marketplace.Application.Services.Contracts
{
    public interface IProductOfferApplicationService : IApplicationService
        <PostProductOfferDto,PutProductOfferDto,DeleteProductOfferDto,GetByIdProductOfferDto,GetAllProductOfferDto>
    {
    }
}
