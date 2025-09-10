using BuildingBlocks.CQRS;
using Catalog.Api.Exceptions;
using Catalog.Api.Models;
using Marten;
using Marten.Linq.QueryHandlers;

namespace Catalog.Api.Products.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IQuery<Product>;
    internal class GetProductByIdHandler : IQueryHandler<GetProductByIdQuery, Product>
    {
        private readonly IQuerySession _session;
        public GetProductByIdHandler(IQuerySession session)
        {
            _session = session;
        }

        public async Task<Product> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            Product product = await _session.LoadAsync<Product>(query.Id, cancellationToken);
            if (product == null) {
                throw new ProductNotFoundException("product not found");
            }
            

            return product;
        }
    }
}
