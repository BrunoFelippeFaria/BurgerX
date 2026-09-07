using BurgerX.Application.Shared.Interfaces;

using FluentValidation;

using Mediator;

namespace BurgerX.Application.Catalog.Products.Commands.Create;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
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