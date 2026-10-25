using Student_Affair_DDD.Modules.Teachers.Application.Common;

namespace Student_Affair_DDD.Modules.Teachers.Persistence;

internal sealed class TeachersRepository(TeachersDbContext db) : ITeacherRepository
{
    public Task<Teacher?> GetByIdAsync(int id, CancellationToken cancellationToken = default) => db.Teachers.FindAsync([id], cancellationToken).AsTask();
    public Task<bool> EmployeeNumberExistsAsync(string number, int? excludeId = null, CancellationToken cancellationToken = default) => db.Teachers.AnyAsync(t => t.EmployeeNumber == number.Trim().ToUpper() && (excludeId == null || t.Id != excludeId), cancellationToken);
    public Task<bool> EmailExistsAsync(string email, int? excludeId = null, CancellationToken cancellationToken = default) => db.Teachers.AnyAsync(t => t.Email == email.Trim().ToLower() && (excludeId == null || t.Id != excludeId), cancellationToken);
    public Task AddAsync(Teacher teacher, CancellationToken cancellationToken = default) => db.Teachers.AddAsync(teacher, cancellationToken).AsTask();
    public void Update(Teacher teacher) => db.Teachers.Update(teacher);
    public void Remove(Teacher teacher) => db.Teachers.Remove(teacher);
    public async Task<(IReadOnlyList<Teacher> Items, int TotalCount)> GetPagedAsync(TeacherPaginationSpecParams spec, CancellationToken cancellationToken = default)
    {
        var query = db.Teachers.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(spec.Search))
        {
            var value = spec.Search.Trim();
            query = query.Where(t => t.FirstName.Contains(value) || t.LastName.Contains(value) ||
                                     t.EmployeeNumber.Contains(value) || t.Email.Contains(value));
        }
        if (!string.IsNullOrWhiteSpace(spec.Department)) query = query.Where(t => t.Department == spec.Department.Trim());
        if (spec.IsActive is not null) query = query.Where(t => t.IsActive == spec.IsActive);
        var totalCount = await query.CountAsync(cancellationToken);
        query = spec.SortBy?.ToLowerInvariant() switch
        {
            "employeenumber" => spec.SortDescending ? query.OrderByDescending(t => t.EmployeeNumber) : query.OrderBy(t => t.EmployeeNumber),
            "department" => spec.SortDescending ? query.OrderByDescending(t => t.Department).ThenBy(t => t.LastName) : query.OrderBy(t => t.Department).ThenBy(t => t.LastName),
            _ => spec.SortDescending ? query.OrderByDescending(t => t.LastName).ThenByDescending(t => t.FirstName) : query.OrderBy(t => t.LastName).ThenBy(t => t.FirstName)
        };
        var pageIndex = Math.Max(1, spec.PageIndex);
        var pageSize = Math.Clamp(spec.PageSize, 1, 100);
        var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        return (items, totalCount);
    }
}

