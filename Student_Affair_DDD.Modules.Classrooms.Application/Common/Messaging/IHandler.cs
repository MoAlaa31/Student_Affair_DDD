namespace Student_Affair_DDD.Modules.Classrooms.Application.Common.Messaging;

public interface IHandler<in TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
