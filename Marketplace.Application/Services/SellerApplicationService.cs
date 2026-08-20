using InvoiceApp.Frameworks.ResponseFrameworks;
using InvoiceApp.Frameworks.ResponseFrameworks.Contracts;
using Marketplace.Application.Dtos;
using Marketplace.Application.Dtos.SellerDtos;
using Marketplace.Application.Services.Contracts;
using Marketplace.Domain.Aggregates.PersonAggregates;
using Marketplace.RepositoryDesignPattern.Services.Contracts;
using ResponseFramework;
using System.Net;

namespace Marketplace.Application.Services
{
    public class SellerApplicationService : ISellerApplicationService
    {
        #region [- PrivateFields -]
        private readonly ISellerRepository _sellerRepository;
        #endregion

        #region [- Ctor -]
        public SellerApplicationService(ISellerRepository sellerRepository)
        {
            _sellerRepository = sellerRepository;
        }
        #endregion

        #region [- PostAsync() -]
        public async Task<IResponse<PostSellerDto>> PostAsync(PostSellerDto postSellerDto)
        {
            if (postSellerDto == null)
            {
                return new Response<PostSellerDto>(
                    false,
                    HttpStatusCode.BadRequest,
                    ResponseMessages.Error,
                    null
                    );
            }
            else
            {
                var seller = new Seller()
                {
                    GuidKey = postSellerDto.GuidKey,
                    FirstName = postSellerDto.FirstName,
                    LastName = postSellerDto.LastName,
                    StoreName = postSellerDto.StoreName,
                    BirthDate = postSellerDto.BirthDate,
                };
                var result = await _sellerRepository.InsertAsync(seller);
                if (!result.IsSuccessful)
                {
                    return new Response<PostSellerDto>(
                        false,
                        HttpStatusCode.InternalServerError,
                        ResponseMessages.Error,
                        null);
                }
                else
                {
                    return new Response<PostSellerDto>(
                       true,
                       HttpStatusCode.Created,
                       ResponseMessages.SuccessfullOperation,
                       postSellerDto);
                }
            }
        }
        #endregion

        #region [- PutAsync() -]
        public async Task<IResponse<PutSellerDto>> PutAsync(PutSellerDto putSellerDto)
        {
            if (putSellerDto == null)
            {
                return new Response<PutSellerDto>(
                    false,
                    HttpStatusCode.BadRequest,
                    ResponseMessages.Error,
                    null
                    );
            }
            else
            {
                var seller = new Seller()
                {
                    GuidKey = putSellerDto.GuidKey,
                    FirstName = putSellerDto.FirstName,
                    LastName = putSellerDto.LastName,
                    StoreName = putSellerDto.StoreName,
                    BirthDate = putSellerDto.BirthDate,
                };
                var result = await _sellerRepository.UpdateAsync(seller);
                if (!result.IsSuccessful)
                {
                    return new Response<PutSellerDto>(
                        false,
                        HttpStatusCode.InternalServerError,
                        ResponseMessages.Error,
                        null);
                }
                else
                {
                    return new Response<PutSellerDto>(
                       true,
                       HttpStatusCode.OK,
                       ResponseMessages.SuccessfullOperation,
                       putSellerDto);
                }
            }
        }
        #endregion

        #region [- DeleteAsync() -]
        public async Task<IResponse<DeleteSellerDto>> DeleteAsync(DeleteSellerDto deleteSellerDto)
        {
            if (deleteSellerDto == null)
            {
                return new Response<DeleteSellerDto>(
                    false,
                    HttpStatusCode.BadRequest,
                    ResponseMessages.Error,
                    null
                    );
            }
            else
            {
                var seller = new Seller()
                {
                    GuidKey = deleteSellerDto.GuidKey,
                    FirstName = deleteSellerDto.FirstName,
                    LastName = deleteSellerDto.LastName,
                    StoreName = deleteSellerDto.StoreName,
                    BirthDate = deleteSellerDto.BirthDate,
                };
                var result = await _sellerRepository.DeleteAsync(seller);
                if (!result.IsSuccessful)
                {
                    return new Response<DeleteSellerDto>(
                        false,
                        HttpStatusCode.NotFound,
                        ResponseMessages.NullInput,
                        null);
                }
                else
                {
                    return new Response<DeleteSellerDto>(
                       true,
                       HttpStatusCode.OK,
                       ResponseMessages.SuccessfullOperation,
                       deleteSellerDto
                       );
                }
            }
        }
        #endregion

        #region [- GetByIdAsync() -]
        public async Task<IResponse<GetByIdSellerDto>> GetByIdAsync(GetByIdSellerDto getByIdSellerDto)
        {
            if (getByIdSellerDto == null)
            {
                return new Response<GetByIdSellerDto>(
                    false,
                    HttpStatusCode.BadRequest,
                    ResponseMessages.Error,
                    null
                    );
            }
            else
            {
                var seller = new Seller()
                {
                    Id = getByIdSellerDto.Id,
                    GuidKey = getByIdSellerDto.GuidKey,
                    FirstName = getByIdSellerDto.FirstName,
                    LastName = getByIdSellerDto.LastName,
                    StoreName = getByIdSellerDto.StoreName,
                    BirthDate = getByIdSellerDto.BirthDate,
                };
                var customerDto = await _sellerRepository.SelectByIdAsync(seller);
                if (!customerDto.IsSuccessful || customerDto.Value == null)
                {
                    return new Response<GetByIdSellerDto>(
                        false,
                        HttpStatusCode.NotFound,
                        ResponseMessages.NullInput,
                        null);
                }
                else
                {
                    var responseDto = new GetByIdSellerDto()
                    {
                        GuidKey = customerDto.Value.GuidKey,
                        FirstName = customerDto.Value.FirstName,
                        LastName = customerDto.Value.LastName,
                        StoreName = customerDto.Value.StoreName,
                        BirthDate = customerDto.Value.BirthDate,
                    };
                    return new Response<GetByIdSellerDto>(
                       true,
                       HttpStatusCode.OK,
                       ResponseMessages.SuccessfullOperation,
                       responseDto);
                }
            }
        }
        #endregion

        #region [- GetAllAsync() -]
        public async Task<IResponse<List<GetAllSellerDto>>> GetAllAsync()
        {
            var sellers = await _sellerRepository.SelectAllAsync();
            if (!sellers.IsSuccessful || sellers.Value == null)
            {
                return new Response<List<GetAllSellerDto>>(
                    false,
                    HttpStatusCode.NotFound,
                    ResponseMessages.NullInput,
                    null);
            }
            else
            {
                var result = sellers.Value.Select(seller => new GetAllSellerDto()
                {
                    Id = seller.Id,
                    FirstName = seller.FirstName,
                    LastName = seller.LastName,
                    StoreName = seller.StoreName,
                    BirthDate = seller.BirthDate,
                }).ToList();
                return new Response<List<GetAllSellerDto>>(
                    true,
                    HttpStatusCode.OK,
                    ResponseMessages.SuccessfullOperation,
                    result);
            }
        }
        #endregion
    }
}
