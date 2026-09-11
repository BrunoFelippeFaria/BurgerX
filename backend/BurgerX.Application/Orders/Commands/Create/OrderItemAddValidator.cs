using FluentValidation;

namespace BurgerX.Application.Orders.Commands.Create;

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