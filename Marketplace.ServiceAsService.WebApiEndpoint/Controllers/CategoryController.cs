using InvoiceApp.ApplicationServices.Dtos.CustomerDtos;
using Marketplace.Application.Dtos;
using Marketplace.Application.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.ServiceAsService.WebApiEndpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        #region [- PrivateFields -]
        private readonly ICategoryApplicationService _categoryApplicationService;
        private readonly ILogger<CategoryController> _logger;
        #endregion

        #region [- Ctor -]
        public CategoryController(ICategoryApplicationService categoryApplicationService, ILogger<CategoryController> logger)
        {
            _categoryApplicationService = categoryApplicationService;
            _logger = logger;
        }
        #endregion

        #region [- Post() -]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostCategoryDto postCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Validation failed for postCategoryDto: {@ModelState}", ModelState);
                return BadRequest(ModelState);
            }


            var result = await _categoryApplicationService.PostAsync(postCategoryDto);

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
        public async Task<IActionResult> Put([FromBody] PutCategoryDto putCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning(
                    "Validation failed while updating customer. Data: {@ModelState}",
                    ModelState);

                return BadRequest(ModelState);
            }

            var result = await _categoryApplicationService.PutAsync(putCategoryDto);

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
        public async Task<IActionResult> Delete([FromBody] DeleteCategoryDto deleteCategoryDto)
        {
            if (deleteCategoryDto.GuidKey == Guid.Empty)
            {
                _logger.LogWarning(
                    "DeleteCustomer request failed due to empty CustomerID");

                return BadRequest("Null CustomerID!");
            }

            _logger.LogInformation(
                "DeleteCustomer request received. CustomerId: {CustomerId}",
                deleteCategoryDto.GuidKey);

            var result = await _categoryApplicationService.DeleteAsync(deleteCategoryDto);

            if (!result.IsSuccessful)
            {
                _logger.LogWarning(
                    "Customer deletion failed. CustomerId: {CustomerId}, Message: {Message}",
                    deleteCategoryDto.GuidKey,
                    result.Message);

                return BadRequest(result.Message);
            }

            _logger.LogInformation(
                "Customer deleted successfully. CustomerId: {CustomerId}",
                deleteCategoryDto.GuidKey);

            return Ok(result.Value);
        }
        #endregion

        #region [- GetById() -]
        [HttpGet("GetCustomerByIdDto")]
        public async Task<IActionResult> GetById([FromQuery] GetByIdCategoryDto getByIdCategoryDto)
        {
            if (getByIdCategoryDto.GuidKey == Guid.Empty)
            {
                _logger.LogWarning(
                    "GetCustomerById failed due to empty CustomerID");

                return BadRequest("Null CustomerID");
            }

            _logger.LogInformation(
                "GetCustomerById request received. CustomerId: {CustomerId}",
                getByIdCategoryDto.GuidKey);

            var result = await _categoryApplicationService.GetByIdAsync(getByIdCategoryDto);

            if (!result.IsSuccessful)
            {
                _logger.LogWarning(
                    "GetCustomerById failed. CustomerId: {CustomerId}, Message: {Message}",
                    getByIdCategoryDto.GuidKey,
                    result.Message);

                return BadRequest(result.Message);
            }

            _logger.LogInformation(
                "GetCustomerById succeeded. CustomerId: {CustomerId}",
                getByIdCategoryDto.GuidKey);

            return Ok(result.Value);
        }
        #endregion

        #region [- GetAll() -]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _categoryApplicationService.GetAllAsync();

            var response = result.Value;

            return Ok(response);
        }
        #endregion
    }
}
