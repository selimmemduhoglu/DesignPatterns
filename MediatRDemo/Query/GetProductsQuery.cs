using MediatR;
using MediatRDemo.Models;

namespace MediatRDemo.Query;

// 1. Query örneği - Veri alımı için
public class GetProductsQuery : IRequest<List<ProductDto>>
{
    public bool IncludeOutOfStock { get; set; }

}
