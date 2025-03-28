﻿using MediatR;
using MediatRDemo.Command;
using MediatRDemo.Context;
using MediatRDemo.Models;
using System;

namespace MediatRDemo.Handlers;

// Command Handler - Veriyi değiştiren handler
//IRequestHandler<TRequest, TResponse>: Command ve query işlemlerini gerçekleştiren sınıflar
public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{
	private readonly AppDbContext _dbContext;

	public CreateProductCommandHandler(AppDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
	{
		Product product = new Product
		{
			Name = request.Name,
			Price = request.Price,
			Stock = request.Stock
		};

		_dbContext.Products.Add(product);
		await _dbContext.SaveChangesAsync(cancellationToken);

		return product.Id;
	}
}
