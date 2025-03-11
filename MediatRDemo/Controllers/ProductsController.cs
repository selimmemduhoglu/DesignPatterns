using MediatR;
using MediatRDemo.Command;
using MediatRDemo.Models;
using MediatRDemo.Notification;
using MediatRDemo.Query;
using Microsoft.AspNetCore.Mvc;

namespace MediatRDemo.Controllers;

// Controller - MediatR'ı API içinde kullanma
[ApiController]
[Route("api/[controller]")]
public class ProductsController(IMediator _mediator) : ControllerBase
{

	[HttpGet]
	public async Task<ActionResult<List<ProductDto>>> GetProducts([FromQuery] bool includeOutOfStock = false)
	{
		GetProductsQuery query = new GetProductsQuery { IncludeOutOfStock = includeOutOfStock };
		List<ProductDto> result = await _mediator.Send(query);
		return Ok(result);
	}

	[HttpPost]
	public async Task<ActionResult<int>> CreateProduct([FromBody] CreateProductCommand command)
	{
		int productId = await _mediator.Send(command);

		// Notification gönderme
		//Publish(): Bir olayı birden çok handler'a gönderir
		//Bir notification'ın birden çok handler'ı olabilir (1:N ilişkisi)
		await _mediator.Publish(new ProductCreatedNotification
		{
			ProductId = productId,
			ProductName = command.Name
		});

		return Ok(productId);
	}
}
