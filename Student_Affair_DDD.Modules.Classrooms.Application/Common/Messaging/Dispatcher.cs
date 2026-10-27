using Microsoft.Extensions.DependencyInjection;

namespace Student_Affair_DDD.Modules.Classrooms.Application.Common.Messaging;

public sealed class Dispatcher : IDispatcher
{
    private readonly IServiceProvider _provider;
    public Dispatcher(IServiceProvider provider) => _provider = provider;

    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        var requestType = request.GetType();
        var handlerType = typeof(IHandler<,>).MakeGenericType(requestType, typeof(TResponse));
        var handler = (dynamic)_provider.GetRequiredService(handlerType);
        return await handler.Handle((dynamic)request, cancellationToken);
    }
}
