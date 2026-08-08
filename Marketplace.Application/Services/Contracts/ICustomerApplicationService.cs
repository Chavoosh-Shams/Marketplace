using Marketplace.Application.Services.Contracts;
using InvoiceApp.ApplicationServices.Dtos.CustomerDtos;

namespace InvoiceApp.ApplicationServices.Services.Contracts
{
    public interface ICustomerApplicationService 
        :IApplicationService<PostCustomerDto, PutCustomerDto, DeleteCustomerDto, GetCustomerByIdDto, GetAllCustomerDto>
    {

    }
}
