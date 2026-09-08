using BurgerX.Application.Orders.Commands.CreateOrder;
using BurgerX.Application.Orders.Queries.GetAll;
using BurgerX.Application.Orders.Queries.GetById;

using Mediator;

using Microsoft.AspNetCore.Mvc;

namespace BurgerX.Api.Controllers;

[ApiController]
[Route("orders")]
public class OrdersController (IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _mediator.Send(new GetAllOrdersQuery());
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var orders = await _mediator.Send(new GetOrderByIdQuery(id));
        return Ok(orders);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderCommand command)
    {
        var id = await _mediator.Send(command);
        
        return Created(
            $"/orders/{id}",
            new { id }
        );
    }
}