using Catalog.Api.Products.CreateProduct;
using static Catalog.Api.Controllers.ProductController;

namespace Catalog.Api.Mappers
{
    public static class CreateCommandMapper
    {
        public static CreateProductCommand ToCreateProductCommand(this CreateProductRequest request)
        {
            return new CreateProductCommand(
                    request.Name,
                    request.Category,
                    request.Description,
                    request.ImageFile,
                    request.Price
                )

;
        }
    }
}
