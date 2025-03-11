﻿using MediatR;

namespace MediatRDemo.Notification;

public class ProductCreatedNotification : INotification
{
	public int ProductId { get; set; }
	public string ProductName { get; set; }
}
