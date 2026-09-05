using BurgerX.Application.Catalog.Commands.CreateProduct;
using BurgerX.Application.Catalog.Commands.DeleteProduct;
using BurgerX.Application.Catalog.Commands.UpdateProduct;
using BurgerX.Application.Catalog.Queries.GetAllProducts;
using BurgerX.Application.Catalog.Queries.GetProductById;

using Mediator;

using Microsoft.AspNetCore.Mvc;

namespace BurgerX.Api.Controllers.Catalog;

[ApiController]
[Route("catalog/products")]
public class ProductsController (IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _mediator.Send(new GetAllProductsQuery());
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var products = await _mediator.Send(new GetProductByIdQuery(id));
        return Ok(products);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateProductCommand command)
    {
        await _mediator.Send(command with { Id = id });
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteProductCommand(id));
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductCommand command)
    {
        var id = await _mediator.Send(command);
        return Created($"catalog/products/{id}", new { id });
    }
}