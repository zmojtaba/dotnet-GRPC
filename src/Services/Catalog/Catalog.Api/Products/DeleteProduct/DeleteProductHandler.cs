using BuildingBlocks.CQRS;
using Catalog.Api.Exceptions;
using Catalog.Api.Models;
using FluentValidation;
using Marten;

namespace Catalog.Api.Products.DeleteProduct
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty().WithMessage("Id can not be empty");
        }
    }
    public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsDeleted);
    public class DeleteProductHandler : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        private readonly IDocumentSession _session;
        public DeleteProductHandler(IDocumentSession session)
        {
            _session = session;
        }
        public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            Product? product = await _session.LoadAsync<Product>(request.Id, cancellationToken);
            if (product == null)
            {
                throw new ProductNotFoundException("product not found");
            }
            _session.Delete(product);
            await _session.SaveChangesAsync(cancellationToken);
            return new DeleteProductResult(true);
        }
    }
}
