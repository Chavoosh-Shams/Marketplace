using InvoiceApp.ApplicationServices.Dtos.OrderHeaderDtos;
using Marketplace.Application.Services.Contracts;

namespace InvoiceApp.ApplicationServices.Services.Contracts
{
    public interface IOrderHeaderApplicationService
        : IApplicationService<PostOrderHeaderDto, PutOrderHeaderDto, DeleteOrderHeaderDto, GetOrderHeaderByIdDto, GetAllOrderHeaderDto>
    {

    }
}
