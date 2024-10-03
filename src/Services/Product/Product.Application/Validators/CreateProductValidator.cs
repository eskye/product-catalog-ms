using FluentValidation;
using Product.Application.Requests;

namespace Product.Application.Validators
{
    public class CreateProductValidator : AbstractValidator<CreateProductRequest>
    {
		public CreateProductValidator()
		{
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100).WithMessage("Name is required");
            RuleFor(x => x.Description).MaximumLength(200);
            RuleFor(x => x.Unit).NotEmpty();
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        } 
	}
}

