using MediatR;

namespace MediatRDemo.Behavior;

// 5. Loglama davranışı - İşlem süresi ölçme ve loglama
public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
	private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

	public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
	{
		_logger = logger;
	}

	public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
	{
		_logger.LogInformation($"Handling {typeof(TRequest).Name}");

		var startTime = DateTime.UtcNow;
		var response = await next();
		var duration = DateTime.UtcNow - startTime;

		_logger.LogInformation($"Handled {typeof(TRequest).Name} in {duration.TotalMilliseconds}ms");

		return response;
	}
}
