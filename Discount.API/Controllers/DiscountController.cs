using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class DiscountController : ControllerBase
{
    private readonly IDiscountRepository _repository;

    public DiscountController(IDiscountRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("{productName}", Name = "GetDiscount")]
    public async Task<ActionResult<Coupon>> GetDiscount(string productName)
    {
        return Ok(await _repository.GetDiscount(productName));
    }

    [HttpPost]
    public async Task<ActionResult<Coupon>> CreateDiscount([FromBody] Coupon coupon)
    {
        return CreatedAtRoute("GetDiscount", await _repository.CreateDiscount(coupon));
    }

    [HttpPut]
    public async Task<ActionResult<Coupon>> UpdateDiscount([FromBody] Coupon coupon)
    {
        return Ok(await _repository.UpdateDiscount(coupon));
    }

    [HttpDelete("{productName}")]
    public async Task<ActionResult<Coupon>> DeleteDiscount(string productName)
    {
        return Ok(await _repository.DeleteDiscount(productName));
    }
}
