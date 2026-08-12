using System.Net;
using ResponseFramework;
using InvoiceApp.Frameworks.ResponseFrameworks;
using Marketplace.Domain.Aggregates.PersonAggregates;
using InvoiceApp.ApplicationServices.Dtos.CustomerDtos;
using InvoiceApp.ApplicationServices.Services.Contracts;
using InvoiceApp.Frameworks.ResponseFrameworks.Contracts;
using Marketplace.RepositoryDesignPattern.Services.Contracts;

namespace Marketplace.Application.Services
{
    public class CustomerApplicationService : ICustomerApplicationService
    {
        #region [- PrivateField -]
        private readonly ICustomerRepository _customerRepository;
        #endregion

        #region [- Ctor -]
        public CustomerApplicationService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        #endregion

        #region [- PostAsync() -]
        public async Task<IResponse<PostCustomerDto>> PostAsync(PostCustomerDto postCustomerDto)
        {
            if (postCustomerDto == null)
            {
                return new Response<PostCustomerDto>(
                    false,
                    HttpStatusCode.BadRequest,
                    ResponseMessages.Error,
                    null
                    );
            }
            else
            {
                var customer = new Customer()
                {
                    GuidKey = postCustomerDto.GuidKey,
                    FirstName = postCustomerDto.FirstName,
                    LastName = postCustomerDto.LastName,
                    Phone = postCustomerDto.Phone,
                    City = postCustomerDto.City,
                    Address = postCustomerDto.Address,
                };
                var result = await _customerRepository.InsertAsync(customer);
                if (!result.IsSuccessful)
                {
                    return new Response<PostCustomerDto>(
                        false,
                        HttpStatusCode.InternalServerError,
                        ResponseMessages.Error,
                        null);
                }
                else
                {
                    return new Response<PostCustomerDto>(
                       true,
                       HttpStatusCode.Created,
                       ResponseMessages.SuccessfullOperation,
                       postCustomerDto);
                }
            }
        }
        #endregion

        #region [- PutAsync() -]
        public async Task<IResponse<PutCustomerDto>> PutAsync(PutCustomerDto putCustomerDto)
        {

            if (putCustomerDto == null)
            {
                return new Response<PutCustomerDto>(
                    false,
                    HttpStatusCode.BadRequest,
                    ResponseMessages.Error,
                    null
                    );
            }
            else
            {
                var customer = new Customer()
                {
                    GuidKey = putCustomerDto.GuidKey,
                    FirstName = putCustomerDto.FirstName,
                    LastName = putCustomerDto.LastName,
                    Phone = putCustomerDto.Phone,
                    City = putCustomerDto.City,
                    Address = putCustomerDto.Address,
                };
                var result = await _customerRepository.UpdateAsync(customer);
                if (!result.IsSuccessful)
                {
                    return new Response<PutCustomerDto>(
                        false,
                        HttpStatusCode.InternalServerError,
                        ResponseMessages.Error,
                        null);
                }
                else
                {
                    return new Response<PutCustomerDto>(
                       true,
                       HttpStatusCode.OK,
                       ResponseMessages.SuccessfullOperation,
                       putCustomerDto);
                }
            }
        }
        #endregion

        #region [- DeleteAsync() -]
        public async Task<IResponse<DeleteCustomerDto>> DeleteAsync(DeleteCustomerDto deleteCustomerDto)
        {
            if (deleteCustomerDto == null)
            {
                return new Response<DeleteCustomerDto>(
                    false,
                    HttpStatusCode.BadRequest,
                    ResponseMessages.Error,
                    null
                    );
            }
            else
            {
                var customer = new Customer()
                {
                    GuidKey = deleteCustomerDto.GuidKey,
                    FirstName = deleteCustomerDto.FirstName,
                    LastName = deleteCustomerDto.LastName,
                    Phone = deleteCustomerDto.Phone,
                    City = deleteCustomerDto.City,
                    Address = deleteCustomerDto.Address,
                };
                var result = await _customerRepository.DeleteAsync(customer);
                if (!result.IsSuccessful)
                {
                    return new Response<DeleteCustomerDto>(
                        false,
                        HttpStatusCode.NotFound,
                        ResponseMessages.NullInput,
                        null);
                }
                else
                {
                    return new Response<DeleteCustomerDto>(
                       true,
                       HttpStatusCode.OK,
                       ResponseMessages.SuccessfullOperation,
                       deleteCustomerDto
                       );
                }
            }
        }
        #endregion

        #region [- GetByIdAsync() -]
        public async Task<IResponse<GetCustomerByIdDto>> GetByIdAsync(GetCustomerByIdDto getCustomerByIdDto)
        {
            if (getCustomerByIdDto == null)
            {
                return new Response<GetCustomerByIdDto>(
                    false,
                    HttpStatusCode.BadRequest,
                    ResponseMessages.Error,
                    null
                    );
            }
            else
            {
                var customer = new Customer()
                {
                    GuidKey = getCustomerByIdDto.GuidKey,
                    FirstName = getCustomerByIdDto.FirstName,
                    LastName = getCustomerByIdDto.LastName,
                    Phone = getCustomerByIdDto.Phone,
                    City = getCustomerByIdDto.City,
                    Address = getCustomerByIdDto.Address,
                };
                var customerDto = await _customerRepository.SelectByIdAsync(customer);
                if (!customerDto.IsSuccessful || customerDto.Value == null)
                {
                    return new Response<GetCustomerByIdDto>(
                        false,
                        HttpStatusCode.NotFound,
                        ResponseMessages.NullInput,
                        null);
                }
                else
                {
                    var responseDto = new GetCustomerByIdDto()
                    {
                        GuidKey = customerDto.Value.GuidKey,
                        FirstName = customerDto.Value.FirstName,
                        LastName = customerDto.Value.LastName,
                        Phone = customerDto.Value.Phone,
                        City = customerDto.Value.City,
                        Address = customerDto.Value.Address,
                    };
                    return new Response<GetCustomerByIdDto>(
                       true,
                       HttpStatusCode.OK,
                       ResponseMessages.SuccessfullOperation,
                       responseDto);
                }
            }
        }
        #endregion

        #region [- GetAllAsync() -]
        public async Task<IResponse<List<GetAllCustomerDto>>> GetAllAsync()
        {
            var customers = await _customerRepository.SelectAllAsync();
            if (!customers.IsSuccessful || customers.Value == null)
            {
                return new Response<List<GetAllCustomerDto>>(
                    false,
                    HttpStatusCode.NotFound,
                    ResponseMessages.NullInput,
                    null);
            }
            else
            {
                var result = customers.Value.Select(customer => new GetAllCustomerDto()
                {
                    GuidKey = customer.GuidKey,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Phone = customer.Phone,
                    City = customer.City,
                    Address = customer.Address,
                }).ToList();
                return new Response<List<GetAllCustomerDto>>(
                    true,
                    HttpStatusCode.OK,
                    ResponseMessages.SuccessfullOperation,
                    result);
            }
        }
        #endregion
    }
}
