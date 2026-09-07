using BurgerX.Application.Orders.Dtos;

using FluentValidation;

namespace BurgerX.Application.Orders.Commands.CreateOrder;

public class OrderItemAddValidator : AbstractValidator<OrderItemAddDto>
{
    public OrderItemAddValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty();

        RuleFor(x => x.Quantity)
            .GreaterThan(0);

        RuleFor(x => x.Note)
            .MaximumLength(300);
    }
}