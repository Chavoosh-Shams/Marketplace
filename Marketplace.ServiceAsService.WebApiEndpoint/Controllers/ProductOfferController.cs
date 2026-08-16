using Microsoft.AspNetCore.Mvc;
using Marketplace.Application.Services.Contracts;
using Marketplace.Application.Dtos.ProductOfferDtos;

namespace Marketplace.ServiceAsService.WebApiEndpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductOfferController : ControllerBase
    {
        #region [- PrivateFields -]
        private readonly IProductOfferApplicationService _productOfferApplicationService;
        #endregion

        #region [- Ctor -]
        public ProductOfferController(IProductOfferApplicationService productOfferApplicationService)
        {
            _productOfferApplicationService = productOfferApplicationService;
        } 
        #endregion

        #region [- Post() -]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostProductOfferDto postProductOfferDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _productOfferApplicationService.PostAsync(postProductOfferDto);

            if (!result.IsSuccessful)
                return BadRequest(result.Message);

            var response = result.Value;

            return Ok(response);
        }
        #endregion

        #region [- Put() -]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] PutProductOfferDto putProductOfferDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _productOfferApplicationService.PutAsync(putProductOfferDto);

            if (!result.IsSuccessful)
                return BadRequest(result.Message);

            var response = result.Value;

            return Ok(response);
        }
        #endregion

        #region [- Delete() -]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteProductOfferDto deleteProductOfferDto)
        {
            if (deleteProductOfferDto.GuidKey == Guid.Empty)
            {
                return BadRequest("Null CustomerID!");
            }

            var result = await _productOfferApplicationService.DeleteAsync(deleteProductOfferDto);

            if (!result.IsSuccessful)
                return BadRequest(result.Message);

            var response = result.Value;

            return Ok(response);
        }
        #endregion

        #region [- GetById() -]
        [HttpGet("GetProductOfferById")]
        public async Task<IActionResult> GetById([FromQuery] GetByIdProductOfferDto getByIdProductOfferDto)
        {

            if (getByIdProductOfferDto.GuidKey == Guid.Empty)
            {
                return BadRequest("Null OrderHeaderID");
            }

            var result = await _productOfferApplicationService.GetByIdAsync(getByIdProductOfferDto);

            if (!result.IsSuccessful)
                return BadRequest(result.Message);

            var response = result.Value;

            return Ok(response);
        }
        #endregion

        #region [- GetAll() -]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productOfferApplicationService.GetAllAsync();

            var response = result.Value;

            return Ok(response);
        }
        #endregion
    }
}
