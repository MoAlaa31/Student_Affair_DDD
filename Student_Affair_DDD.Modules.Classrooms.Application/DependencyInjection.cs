using Microsoft.Extensions.DependencyInjection;
using Student_Affair_DDD.Modules.Classrooms.Application.Common.Messaging;
using Student_Affair_DDD.Modules.Classrooms.Application.Services;

namespace Student_Affair_DDD.Modules.Classrooms.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddClassroomsApplication(this IServiceCollection services)
    {
        services.AddScoped<IDispatcher, Dispatcher>();
        services.AddScoped<IClassroomService, ClassroomServiceDispatcherAdapter>();
        var assembly = typeof(DependencyInjection).Assembly;
        foreach (var implementation in assembly.GetTypes().Where(t => t is { IsAbstract: false, IsInterface: false }))
        foreach (var contract in implementation.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHandler<,>)))
            services.AddScoped(contract, implementation);
        return services;
    }
}
