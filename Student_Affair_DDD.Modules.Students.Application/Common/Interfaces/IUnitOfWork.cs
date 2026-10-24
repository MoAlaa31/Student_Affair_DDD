using Student_Affair_DDD.Modules.Students.Application.Common.Interfaces.EntitiesIRepos;

namespace Student_Affair_DDD.Modules.Students.Application.Common.Interfaces;

public interface IUnitOfWork : IAsyncDisposable
{
    IStudentRepository Students { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    Task BeginTransactionAsync(CancellationToken cancellationToken = default);

    Task CommitTransactionAsync(CancellationToken cancellationToken = default);

    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}
