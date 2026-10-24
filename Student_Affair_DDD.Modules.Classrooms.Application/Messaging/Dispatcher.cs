using Microsoft.Extensions.DependencyInjection;
using Student_Affair_DDD.Modules.Classrooms.Application.Services;

namespace Student_Affair_DDD.Modules.Classrooms.Application.Messaging;

public interface IRequest<TResponse> { }
public interface IHandler<in TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
public interface IClassroomsDispatcher
{
    Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
}

internal sealed class ClassroomsDispatcher(IServiceProvider services) : IClassroomsDispatcher
{
    public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(IHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        dynamic handler = services.GetRequiredService(handlerType);
        return (Task<TResponse>)handler.Handle((dynamic)request, cancellationToken);
    }
}

public static class ClassroomsModule
{
    public static IServiceCollection AddClassroomsApplication(this IServiceCollection services)
    {
        services.AddScoped<IClassroomsDispatcher, ClassroomsDispatcher>();
        services.AddScoped<IClassroomService, ClassroomServiceDispatcherAdapter>();
        var assembly = typeof(ClassroomsModule).Assembly;
        foreach (var implementation in assembly.GetTypes().Where(t => t is { IsAbstract: false, IsInterface: false }))
        foreach (var contract in implementation.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHandler<,>)))
            services.AddScoped(contract, implementation);
        return services;
    }
}
