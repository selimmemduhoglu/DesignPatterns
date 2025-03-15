using MediatR;
using MediatRDemo.Context;
using MediatRDemo.Models;
using MediatRDemo.Query;
using Microsoft.EntityFrameworkCore;
using System;

namespace MediatRDemo.Handlers;

// Query Handler - Veriyi getiren handler
//IRequestHandler<TRequest, TResponse>: Command ve query işlemlerini gerçekleştiren sınıflar
public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductDto>>
{
	private readonly AppDbContext _dbContext;

	public GetProductsQueryHandler(AppDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<List<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
	{
		IQueryable<Product> query = _dbContext.Products;

	
		return await query.Select(p => new ProductDto
		{
			Id = p.Id,
			Name = p.Name,
			Price = p.Price,
			Stock = p.Stock
		}).ToListAsync(cancellationToken);
	}
}
