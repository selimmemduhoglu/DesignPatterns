using MediatR;

namespace MediatRDemo.Command;

// 2. Command örneği - Veri değişikliği için
//IRequest<T>: Hem command hem query'ler için temel arayüz
public class CreateProductCommand : IRequest<int>
{
	public string Name { get; set; }
	public decimal Price { get; set; }
	public int Stock { get; set; }
}
