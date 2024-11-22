using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.Application.Product.Commands;
using OnlineShop.Core.Application.Product.Queries;

namespace OnlineShop.EndPoint.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult> GetAllProducts()
        {
            var query = new GetAllProductsQuery();
            var products = await _mediator.Send(query);
            return Ok(products);
        }

        /// <summary>
        /// Retrieves a specific product by ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetProductById(Guid id)
        {
            var query = new GetProductByIdQuery(id);
            var product = await _mediator.Send(query);
            if (product == null)
                return NotFound($"Product with ID {id} not found.");
            return Ok(product);
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            var productId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetProductById), new { id = productId }, productId);
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct( [FromBody] UpdateProductCommand command)
        {
            var updatedProductId = await _mediator.Send(command);
            return Ok(updatedProductId);
        }

        /// <summary>
        /// Deletes a product by ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var command = new DeleteProductCommand(id);
            var deletedProductId = await _mediator.Send(command);
            return Ok(deletedProductId);
        }
    }
}
