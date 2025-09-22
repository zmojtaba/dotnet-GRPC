using BuildingBlocks.Exceptions;

namespace Catalog.Api.Exceptions
{
    public class ProductNotFoundException : NotFoundException
    {
       
        public ProductNotFoundException(string message) : base(message)
        {
        }
        public ProductNotFoundException(Guid Id) : base("product", Id)
        {
        }
    }
}
