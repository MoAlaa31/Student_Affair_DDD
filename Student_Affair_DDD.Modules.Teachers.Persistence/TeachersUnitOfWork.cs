using Microsoft.EntityFrameworkCore.Storage;

namespace Student_Affair_DDD.Modules.Teachers.Persistence;

internal sealed class TeachersUnitOfWork(TeachersDbContext context, ITeacherRepository teachers) : IUnitOfWork
{
    private IDbContextTransaction? transaction;
    public ITeacherRepository Teachers { get; } = teachers;
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => context.SaveChangesAsync(cancellationToken);

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        => transaction ??= await context.Database.BeginTransactionAsync(cancellationToken);

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (transaction is null) return;
        try
        {
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        finally
        {
            await transaction.DisposeAsync();
            transaction = null;
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (transaction is null) return;
        try { await transaction.RollbackAsync(cancellationToken); }
        finally { await transaction.DisposeAsync(); transaction = null; }
    }

    public async ValueTask DisposeAsync()
    {
        if (transaction is null) return;
        await transaction.DisposeAsync();
        transaction = null;
    }
}
