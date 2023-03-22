using Basket.API.Entities;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class BasketController : Controller
    {
        private readonly IBasketRepository _repository;

        public BasketController(IBasketRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("username")]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string username)
        {
            var cart = await _repository.GetBasketAsync(username);

            return Ok(cart ?? new ShoppingCart(username));
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart cart)
        {
            return Ok(await _repository.UpdateBasketAsync(cart));
        }

        [HttpDelete("username")]
        public async Task<IActionResult> DeleteBasket(string username)
        {
            await _repository.DeleteBasketAsync(username);

            return StatusCode(204);
        }
    }
}
