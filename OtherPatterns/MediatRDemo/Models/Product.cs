namespace MediatRDemo.Models;

// Entity Framework Core için veritabanı modelleri
public class Product
{
	public int Id { get; set; }
	public string Name { get; set; }
	public decimal Price { get; set; }
	public int Stock { get; set; }
}
