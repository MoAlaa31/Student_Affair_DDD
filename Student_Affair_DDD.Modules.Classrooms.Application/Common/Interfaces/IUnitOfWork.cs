
namespace Student_Affair_DDD.Modules.Classrooms.Application.Common.Interfaces;

public interface IUnitOfWork : IAsyncDisposable
{
    IClassroomRepository Classrooms { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}
