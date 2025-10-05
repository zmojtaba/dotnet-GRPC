using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers
{
    [Route("api/basket")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllBasket()
        {
            return StatusCode(200, "get all");
        }
    }
}
