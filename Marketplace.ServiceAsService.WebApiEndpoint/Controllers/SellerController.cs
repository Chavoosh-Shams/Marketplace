using InvoiceApp.ApplicationServices.Dtos.CustomerDtos;
using InvoiceApp.ApplicationServices.Services.Contracts;
using Marketplace.Application.Dtos.SellerDtos;
using Marketplace.Application.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.ServiceAsService.WebApiEndpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        #region [- PrivateField -]
        private readonly ISellerApplicationService _sellerApplicationService;
        private readonly ILogger<SellerController> _logger;
        #endregion

        #region [- Ctor -]
        public SellerController(ISellerApplicationService sellerApplicationService, ILogger<SellerController> logger)
        {
            _sellerApplicationService = sellerApplicationService;
            _logger = logger;
        }
        #endregion

        #region [- Post() -]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostSellerDto postSellerDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Validation failed for PostCustomerDto: {@ModelState}", ModelState);
                return BadRequest(ModelState);
            }


            var result = await _sellerApplicationService.PostAsync(postSellerDto);

            if (!result.IsSuccessful)
            {
                _logger.LogWarning("Customer creation failed: {Message}", result.Message);
                return BadRequest(result.Message);
            }

            var response = result.Value;

            _logger.LogInformation("Customer created successfully. CustomerId: {CustomerId}", result.Value);

            return Ok(response);
        }
        #endregion

        #region [- Put() -]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] PutSellerDto putSellerDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning(
                    "Validation failed while updating customer. Data: {@ModelState}",
                    ModelState);

                return BadRequest(ModelState);
            }

            var result = await _sellerApplicationService.PutAsync(putSellerDto);

            if (!result.IsSuccessful)
            {
                _logger.LogWarning(
                    "Customer update failed. Message: {Message},", result.Message);

                return BadRequest(result.Message);
            }

            _logger.LogInformation(
                "Customer updated successfully. CustomerId: {CustomerId}", result.Value);

            return Ok(result.Value);
        }
        #endregion

        #region [- Delete() -]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteSellerDto deleteSellerDto)
        {
            if (deleteSellerDto.GuidKey == Guid.Empty)
            {
                _logger.LogWarning(
                    "DeleteCustomer request failed due to empty CustomerID");

                return BadRequest("Null CustomerID!");
            }

            _logger.LogInformation(
                "DeleteCustomer request received. CustomerId: {CustomerId}",
                deleteSellerDto.GuidKey);

            var result = await _sellerApplicationService.DeleteAsync(deleteSellerDto);

            if (!result.IsSuccessful)
            {
                _logger.LogWarning(
                    "Customer deletion failed. CustomerId: {CustomerId}, Message: {Message}",
                    deleteSellerDto.GuidKey,
                    result.Message);

                return BadRequest(result.Message);
            }

            _logger.LogInformation(
                "Customer deleted successfully. CustomerId: {CustomerId}",
                deleteSellerDto.GuidKey);

            return Ok(result.Value);
        }
        #endregion

        #region [- GetById() -]
        [HttpGet("GetCustomerByIdDto")]
        public async Task<IActionResult> GetById([FromQuery] GetByIdSellerDto getByIdSellerDto)
        {
            if (getByIdSellerDto.GuidKey == Guid.Empty)
            {
                _logger.LogWarning(
                    "GetCustomerById failed due to empty CustomerID");

                return BadRequest("Null CustomerID");
            }

            _logger.LogInformation(
                "GetCustomerById request received. CustomerId: {CustomerId}",
                getByIdSellerDto.GuidKey);

            var result = await _sellerApplicationService.GetByIdAsync(getByIdSellerDto);

            if (!result.IsSuccessful)
            {
                _logger.LogWarning(
                    "GetCustomerById failed. CustomerId: {CustomerId}, Message: {Message}",
                    getByIdSellerDto.GuidKey,
                    result.Message);

                return BadRequest(result.Message);
            }

            _logger.LogInformation(
                "GetCustomerById succeeded. CustomerId: {CustomerId}",
                getByIdSellerDto.GuidKey);

            return Ok(result.Value);
        }
        #endregion

        #region [- GetAll() -]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _sellerApplicationService.GetAllAsync();

            var response = result.Value;

            return Ok(response);
        }
        #endregion
    }
}
