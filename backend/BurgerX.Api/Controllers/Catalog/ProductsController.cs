using BurgerX.Application.Catalog.Commands.CreateProduct;
using BurgerX.Application.Catalog.Queries.GetAllProducts;

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

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductCommand command)
    {
        var id = await _mediator.Send(command);
        return Created($"catalog/products/{id}", new { id });
    }
}