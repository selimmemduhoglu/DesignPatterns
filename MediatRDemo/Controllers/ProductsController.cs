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
public class ProductsController : ControllerBase
{
	private readonly IMediator _mediator;

	public ProductsController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpGet]
	public async Task<ActionResult<List<ProductDto>>> GetProducts([FromQuery] bool includeOutOfStock = false)
	{
		var query = new GetProductsQuery { IncludeOutOfStock = includeOutOfStock };
		var result = await _mediator.Send(query);
		return Ok(result);
	}

	[HttpPost]
	public async Task<ActionResult<int>> CreateProduct([FromBody] CreateProductCommand command)
	{
		var productId = await _mediator.Send(command);

		// Notification gönderme
		await _mediator.Publish(new ProductCreatedNotification
		{
			ProductId = productId,
			ProductName = command.Name
		});

		return Ok(productId);
	}
}
