using BurgerX.Application.Catalog.Products.Commands.Create;
using BurgerX.Application.Catalog.Products.Commands.Delete;
using BurgerX.Application.Catalog.Products.Commands.Update;
using BurgerX.Application.Catalog.Products.Queries.GetAll;
using BurgerX.Application.Catalog.Products.Queries.GetById;

using Mediator;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BurgerX.Api.Controllers;

[ApiController]
[Route("catalog/products")]
[Authorize]

public class ProductsController(IMediator mediator) : ControllerBase
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