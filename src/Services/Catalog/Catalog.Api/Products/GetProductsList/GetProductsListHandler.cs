using BuildingBlocks.CQRS;
using Catalog.Api.Models;
using Marten;

namespace Catalog.Api.Products.GetProductsList
{
    public record GetProductsListQuery() : IQuery<GetProductsListResult>;
    public record GetProductsListResult(IEnumerable<Product> products);
    internal class GetProductsListQueryHandler : IQueryHandler<GetProductsListQuery, GetProductsListResult>
    {
        private readonly IQuerySession _session;
        public GetProductsListQueryHandler(IQuerySession session )
        {
            _session = session;
        }

        public async Task<GetProductsListResult> Handle(GetProductsListQuery query, CancellationToken cancellationToken)
        {
            Console.WriteLine($"aaaaaaaaaaaaaaaaa     {query}");
            var products =  await _session.Query<Product>().ToListAsync(cancellationToken);
            return new GetProductsListResult(products);
        }
    }
}
