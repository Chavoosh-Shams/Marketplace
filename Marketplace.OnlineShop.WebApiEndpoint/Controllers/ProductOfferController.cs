using Marketplace.Application.Dtos.ProductOfferDtos;
using Marketplace.Application.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.OnlineShop.WebApiEndpoint.Controllers
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
