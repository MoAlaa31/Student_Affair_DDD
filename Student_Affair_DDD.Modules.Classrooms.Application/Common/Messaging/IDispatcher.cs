namespace Student_Affair_DDD.Modules.Classrooms.Application.Common.Messaging;

public interface IDispatcher
{
    Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
}
