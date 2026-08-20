using InvoiceApp.ApplicationServices.Dtos.OrderHeaderDtos;
using InvoiceApp.ApplicationServices.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.OnlineShop.WebApiEndpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderHeaderController : ControllerBase
    {
        #region [- PrivateField -]
        private readonly IOrderHeaderApplicationService _orderHeaderApplicationService;
        #endregion

        #region [- Ctor -]
        public OrderHeaderController(IOrderHeaderApplicationService orderHeaderApplicationService)
        {
            _orderHeaderApplicationService = orderHeaderApplicationService;
        }
        #endregion

        #region [- Post() -]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostOrderHeaderDto postOrderHeaderDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _orderHeaderApplicationService.PostAsync(postOrderHeaderDto);

            if (!result.IsSuccessful)
                return BadRequest(result.Message);

            var response = result.Value;

            return Ok(response);
        }
        #endregion
    }
}
