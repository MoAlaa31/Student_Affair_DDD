using Student_Affair_DDD.Modules.Teachers.Application.Common.Interfaces.EntitiesIRepos;

namespace Student_Affair_DDD.Modules.Teachers.Application.Common.Interfaces;

public interface IUnitOfWork : IAsyncDisposable
{
    ITeacherRepository Teachers { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}
