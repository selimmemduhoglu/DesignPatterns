using MediatR;
using MediatRDemo.Models;

namespace MediatRDemo.Query;

// 1. Query örneği - Veri alımı için
//IRequest<T>: Hem command hem query'ler için temel arayüz
public class GetProductsQuery : IRequest<List<ProductDto>>
{
	public bool IncludeOutOfStock { get; set; }

}
