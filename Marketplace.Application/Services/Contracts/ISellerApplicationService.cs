

using Marketplace.Application.Dtos.SellerDtos;

namespace Marketplace.Application.Services.Contracts
{
    public interface ISellerApplicationService : IApplicationService
        <PostSellerDto,PutSellerDto,DeleteSellerDto,GetByIdSellerDto,GetAllSellerDto>
    {
    }
}
