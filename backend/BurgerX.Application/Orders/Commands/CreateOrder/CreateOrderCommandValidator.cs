using BurgerX.Domain.Enums;
using FluentValidation;

namespace BurgerX.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Customer)
            .Length(3, 30)
            .NotEmpty();

        RuleFor(x => x.Discount)
            .InclusiveBetween(0, 100);

        RuleFor(x => x.Items)
            .NotEmpty();
    
        RuleForEach(x => x.Items)
            .SetValidator(new OrderItemValidator());

        When(x => x.Type == OrderType.Delivery, () =>
        {
            RuleFor(x => x.DeliveryAddress)
                .NotNull();
        })
    
        .Otherwise(() =>
        {
            RuleFor(x => x.DeliveryAddress)
                .Null();
        });    
    }
}