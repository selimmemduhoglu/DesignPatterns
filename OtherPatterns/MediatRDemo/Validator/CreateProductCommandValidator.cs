using FluentValidation;
using MediatRDemo.Command;

namespace MediatRDemo.Validator;

// Command validator - FluentValidation kullanarak komut doğrulama
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
	public CreateProductCommandValidator()
	{
		RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
		RuleFor(x => x.Price).GreaterThan(0);
		RuleFor(x => x.Stock).GreaterThanOrEqualTo(0);
	}
}
