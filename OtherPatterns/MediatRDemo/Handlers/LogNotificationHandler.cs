using MediatR;
using MediatRDemo.Notification;

namespace MediatRDemo.Handlers;

//INotificationHandler<T>: Notification'ları işlemek için kullanılır

public class LogNotificationHandler : INotificationHandler<ProductCreatedNotification>
{
	public Task Handle(ProductCreatedNotification notification, CancellationToken cancellationToken)
	{
		Console.WriteLine($"Log kaydediliyor: ProductId {notification.ProductId} sisteme eklendi");
		return Task.CompletedTask;
	}
}
