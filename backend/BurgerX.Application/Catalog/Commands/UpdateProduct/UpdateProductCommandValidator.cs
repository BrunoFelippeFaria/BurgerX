using BurgerX.Application.Shared.Interfaces;

using FluentValidation;

using Mediator;

namespace BurgerX.Application.Catalog.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .Length(3, 30);

        RuleFor(x => x.Description)
            .NotEmpty()
            .Length(3, 300);

        RuleFor(x => x.Price)
            .GreaterThan(-1);
    }
}