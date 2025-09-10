using Catalog.Api.Mappers;
using Catalog.Api.Products.CreateProduct;
using Catalog.Api.Products.GetProductByCategory;
using Catalog.Api.Products.GetProductById;
using Catalog.Api.Products.GetProductsList;
using ImTools;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);
        //public record GetProductsListRequest

        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            CreateProductResult result = await _mediator.Send(request.ToCreateProductCommand() );
            return Ok(result);
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetProductsList()
        {
            return Ok(await _mediator.Send(new GetProductsListQuery()));
        }


        [HttpGet("product/{id}")]
        public async Task<IActionResult> GetProductById([FromRoute] Guid id)
        {
            return Ok(await _mediator.Send(new GetProductByIdQuery(id)));

        }

        [HttpGet("products/category/{category}")]
        public async Task<IActionResult> GetProductsByCategory(string category)
        {
            var result = await _mediator.Send(new GetProductByCategoryQuery(category));
            return Ok(result);
        }
    }
}
