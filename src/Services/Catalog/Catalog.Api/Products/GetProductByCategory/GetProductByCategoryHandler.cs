using BuildingBlocks.CQRS;
using Catalog.Api.Models;
using Marten;

namespace Catalog.Api.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(string category) : IQuery<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(IEnumerable<Product> Products);
    internal class GetProductByCategoryHandler : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        private readonly IQuerySession _session;
        public GetProductByCategoryHandler(IQuerySession session)
        {
            _session = session;
        }

        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            var categoryLower = query.category.ToLower();
            var products = await _session.Query<Product>().Where(p => p.Category.Any( c => c.Equals(query.category, StringComparison.OrdinalIgnoreCase) ) ).ToListAsync(cancellationToken);

            return new GetProductByCategoryResult(products);
        }
    }
}
