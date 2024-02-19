using Tekton.Application.Products.Commands.CreateProduct;

namespace Tekton.Application.Products.Validations;
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(50).WithMessage("The Name field must have 50 char Max.")
            .NotEmpty();

        RuleFor(x => x.Description)
            .MaximumLength(150).WithMessage("The Description field must have 150 char Max.")
            .NotEmpty();

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("The Price field must be greater than zero.")
            .NotEmpty();

        RuleFor(x => x.Stock)
            .NotEmpty();

        RuleFor(x => x.StatusName)
            .MaximumLength(10)
            .Must(StatusNameValidation.ValidStatusName).WithMessage("Invalid Status Name specified.")
            .NotEmpty();
    }
}
