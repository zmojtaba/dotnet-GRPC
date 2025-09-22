using BuildingBlocks.CQRS;
using Catalog.Api.Exceptions;
using Catalog.Api.Models;
using FluentValidation;
using Marten;

namespace Catalog.Api.Products.UpdateProductById
{

    public class UpdateProductByIdCommandValidator : AbstractValidator<UpdateProductByIdCommand>
    {
        public UpdateProductByIdCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty().WithMessage("Id can not be empty");
            RuleFor(p => p.Name).MaximumLength(50).WithMessage("Name must be less than 50 characters").When(p => !string.IsNullOrEmpty(p.Name));
            RuleFor(p => p.Description).MaximumLength(500).WithMessage("Description must be less than 500 characters").When(p => !string.IsNullOrEmpty(p.Description));
            RuleFor(p => p.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
            RuleFor(p => p.Category).NotEmpty().WithMessage("Category can not be empty");
            RuleFor(p => p.ImageFile).MaximumLength(100).WithMessage("ImageFile must be less than 100 characters").When(p => !string.IsNullOrEmpty(p.ImageFile));
            RuleForEach(p => p.Category).NotEmpty().WithMessage("Category can not contain empty values");
        }
    }
    public record UpdateProductByIdCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<UpdateProductByIdResult>;
    public record UpdateProductByIdResult(Product product);
    internal class UpdateProductByIdHandler : ICommandHandler<UpdateProductByIdCommand, UpdateProductByIdResult>
    {
        private readonly IDocumentSession _session;

        public UpdateProductByIdHandler(IDocumentSession session)
        {
            _session = session;
        }
        public async Task<UpdateProductByIdResult> Handle(UpdateProductByIdCommand request, CancellationToken cancellationToken)
        {
            Product? product = await _session.LoadAsync<Product>(request.Id, cancellationToken);
            if (product == null)
            {
                throw new ProductNotFoundException("product not found");
            }
            product.Name = request.Name;
            product.Category = request.Category;
            product.Description = request.Description;
            product.ImageFile = request.ImageFile;
            product.Price = request.Price;

            _session.Store(product);
            await _session.SaveChangesAsync();
            return new UpdateProductByIdResult(product);
        }
    }
}
