using FluentValidation;

namespace Catalog.Api.Products.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator() { 
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name can not be empty");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category can not be empty");
            RuleForEach(x => x.Category).NotEmpty().WithMessage("Category item can not be empty");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description can not be empty").MaximumLength(500).WithMessage("Description maximum length is 500 characters");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile can not be empty");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero");
        }
    }
}
