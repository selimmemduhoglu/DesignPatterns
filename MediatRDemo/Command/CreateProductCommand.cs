using MediatR;

namespace MediatRDemo.Command;

// 2. Command örneği - Veri değişikliği için
public class CreateProductCommand : IRequest<int>
{
	public string Name { get; set; }
	public decimal Price { get; set; }
	public int Stock { get; set; }
}
