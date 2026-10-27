using Student_Affair_DDD.Modules.Classrooms.Application.SpecParams;

namespace Student_Affair_DDD.Modules.Classrooms.Persistence;

internal sealed class ClassroomsRepository(ClassroomsDbContext db) : IClassroomRepository
{
    public Task<Classroom?> GetByIdAsync(int id, CancellationToken cancellationToken = default) => db.Classrooms.FindAsync([id], cancellationToken).AsTask();
    public Task<bool> CodeExistsAsync(string code, int? excludeId = null, CancellationToken cancellationToken = default)
        => db.Classrooms.AnyAsync(c => c.Code == code.Trim().ToUpper() && (excludeId == null || c.Id != excludeId), cancellationToken);
    public Task AddAsync(Classroom classroom, CancellationToken cancellationToken = default) => db.Classrooms.AddAsync(classroom, cancellationToken).AsTask();
    public void Update(Classroom classroom) => db.Classrooms.Update(classroom);
    public void Remove(Classroom classroom) => db.Classrooms.Remove(classroom);
    public async Task<(IReadOnlyList<Classroom> Items, int TotalCount)> GetPagedAsync(ClassroomPaginationSpecParams spec, CancellationToken cancellationToken = default)
    {
        var query = db.Classrooms.AsNoTracking().AsQueryable();

        if (spec.IsActive is not null) query = query.Where(c => c.IsActive == spec.IsActive);
        var totalCount = await query.CountAsync(cancellationToken);
        query = spec.SortBy?.ToLowerInvariant() switch
        {
            "code" => spec.SortDescending ? query.OrderByDescending(c => c.Code) : query.OrderBy(c => c.Code),
            "capacity" => spec.SortDescending ? query.OrderByDescending(c => c.Capacity) : query.OrderBy(c => c.Capacity),
            _ => spec.SortDescending ? query.OrderByDescending(c => c.Building).ThenByDescending(c => c.RoomNumber) : query.OrderBy(c => c.Building).ThenBy(c => c.RoomNumber)
        };
        var pageIndex = Math.Max(1, spec.PageIndex);
        var pageSize = Math.Clamp(spec.PageSize, 1, 100);
        var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        return (items, totalCount);
    }
}

