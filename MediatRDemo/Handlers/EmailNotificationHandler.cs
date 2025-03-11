using MediatR;
using MediatRDemo.Notification;

namespace MediatRDemo.Handlers;

// Notification Handler - Birden çok handler olabilir
//INotificationHandler<T>: Notification'ları işlemek için kullanılır

public class EmailNotificationHandler : INotificationHandler<ProductCreatedNotification>
{
	public Task Handle(ProductCreatedNotification notification, CancellationToken cancellationToken)
	{
		Console.WriteLine($"Email gönderiliyor: Yeni ürün eklendi - {notification.ProductName}");
		return Task.CompletedTask;
	}
}