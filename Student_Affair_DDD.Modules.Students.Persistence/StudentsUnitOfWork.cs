using Microsoft.EntityFrameworkCore.Storage;
using Student_Affair_DDD.Modules.Students.Domain.Common;
using System.Collections.Concurrent;

namespace Student_Affair_DDD.Modules.Students.Persistence;

internal sealed class StudentsUnitOfWork : IUnitOfWork
{
    private readonly StudentsDbContext _context;
    private readonly ConcurrentDictionary<Type, object> _repositories = new();
    public StudentsUnitOfWork(StudentsDbContext dbContext)
    {
        this._context = dbContext;
        _repositories = new ConcurrentDictionary<Type, object>();
    }

    private IStudentRepository? _students;
    public IStudentRepository Students => _students ??= new StudentsRepository(_context);

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => _context.SaveChangesAsync(cancellationToken);

    private IDbContextTransaction? _transaction;

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        => _transaction ??= await _context.Database.BeginTransactionAsync(cancellationToken);

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction is null) return;
        try
        {
            await _context.SaveChangesAsync(cancellationToken);
            await _transaction.CommitAsync(cancellationToken);
        }
        finally
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction is null) return;
        try
        {
            await _transaction.RollbackAsync(cancellationToken);
        }
        finally
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_transaction is not null)
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
}
