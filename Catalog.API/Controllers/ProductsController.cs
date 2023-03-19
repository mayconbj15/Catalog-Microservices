using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [Route("/api/v1/[controller]")]
    //[ApiConventionType(typeof(DefaultApiConventions))]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet(Name = "GetProducts")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return Ok(await _repository.GetProductsAsync());
        }

        [HttpGet("{id:length(24)}", Name = "GetProductById")]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var product = await _repository.GetProductByIdAsync(id);

            return product == null ? NotFound() : Ok(product);
        }

        [HttpGet("[action]/{category}", Name = "GetProductsByCategory")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string category)
        {
            var products = await _repository.GetProductsByCategoryAsync(category);

            return products == null ? NotFound() : Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            if(product is null)
            {
                return BadRequest("Invalid product");
            }

            await _repository.CreateProductAsync(product);

            return CreatedAtRoute("GetProductById", product.Id, product);
        }

        [HttpPut("{id:length(24)}", Name = "UpdateProduct")]
        public async Task<ActionResult> UpdateProduct([FromBody] Product product)
        {
            if (product is null)
            {
                return BadRequest("Invalid product");
            }

            return Ok(await _repository.UpdateProductAsync(product));
        }

        [HttpDelete(Name = "DeleteProductById")]
        public async Task<ActionResult> DeleteProductById(string id)
        {
            return Ok(await _repository.DeleteProductAsync(id));
        }
    }
}
