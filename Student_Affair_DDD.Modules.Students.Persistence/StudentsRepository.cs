using Student_Affair_DDD.Modules.Students.Application.SpecParams;
using System.Linq.Expressions;

namespace Student_Affair_DDD.Modules.Students.Persistence;

internal sealed class StudentsRepository : IStudentRepository
{
    private readonly StudentsDbContext Context;
    private readonly DbSet<Student> Set;
    public StudentsRepository(StudentsDbContext context)
    {
        Context = context;
        Set = context.Set<Student>();
    }
    public Task<bool> EmailExistsAsync(string email, int? excludeId = null, CancellationToken cancellationToken = default)
        => Set.AnyAsync(s => s.Email == email.Trim().ToLower() && (excludeId == null || s.Id != excludeId), cancellationToken);

    public Task<bool> StudentNumberExistsAsync(string number, int? excludeId = null, CancellationToken cancellationToken = default)
        => Set.AnyAsync(s => s.StudentNumber == number.Trim() && (excludeId == null || s.Id != excludeId), cancellationToken);

    public Task<bool> NationalIdExistsAsync(string id, int? excludeId = null, CancellationToken cancellationToken = default)
        => Set.AnyAsync(s => s.NationalId == id.Trim() && (excludeId == null || s.Id != excludeId), cancellationToken);

    public async Task<(IReadOnlyList<Student> Items, int TotalCount)> GetPagedAsync(
            StudentPaginationSpecParams studentPagination,
            CancellationToken cancellationToken = default)
    {
        var query = Set.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(studentPagination.Search))
        {
            var term = studentPagination.Search.Trim();
            query = query.Where(s =>
                EF.Functions.Like(s.FirstName, $"%{term}%") ||
                EF.Functions.Like(s.LastName, $"%{term}%") ||
                EF.Functions.Like(s.Email, $"%{term}%") ||
                EF.Functions.Like(s.StudentNumber, $"%{term}%"));
        }

        if (!string.IsNullOrWhiteSpace(studentPagination.department))
            query = query.Where(s => s.Department == studentPagination.department);

        if (studentPagination.isActive is not null)
            query = query.Where(s => s.IsActive == studentPagination.isActive);

        var total = await query.CountAsync(cancellationToken);

        query = (studentPagination.sortBy?.ToLowerInvariant()) switch
        {
            "studentnumber" => studentPagination.sortDescending ? query.OrderByDescending(s => s.StudentNumber) : query.OrderBy(s => s.StudentNumber),
            "department" => studentPagination.sortDescending ? query.OrderByDescending(s => s.Department).ThenBy(s => s.LastName) : query.OrderBy(s => s.Department).ThenBy(s => s.LastName),
            "gpa" => studentPagination.sortDescending ? query.OrderByDescending(s => s.Gpa) : query.OrderBy(s => s.Gpa),
            "enrollmentdate" => studentPagination.sortDescending ? query.OrderByDescending(s => s.EnrollmentDate) : query.OrderBy(s => s.EnrollmentDate),
            _ => studentPagination.sortDescending
                ? query.OrderByDescending(s => s.LastName).ThenByDescending(s => s.FirstName)
                : query.OrderBy(s => s.LastName).ThenBy(s => s.FirstName)
        };

        var items = await query
            .Skip((studentPagination.PageIndex - 1) * studentPagination.PageSize)
            .Take(studentPagination.PageSize)
            .ToListAsync(cancellationToken);

        return (items, total);
    }

    public async Task<IReadOnlyList<string>> GetDepartmentsAsync(CancellationToken cancellationToken = default)
        => await Set.AsNoTracking().Select(s => s.Department).Where(d => d != "").Distinct().OrderBy(d => d).ToListAsync(cancellationToken);
    public Task SaveChanges(CancellationToken cancellationToken) => Context.SaveChangesAsync(cancellationToken);
    public async Task<Student?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
            => await Set.FindAsync([id], cancellationToken);

    public async Task<IReadOnlyList<Student>> ListAllAsync(CancellationToken cancellationToken = default)
        => await Set.AsNoTracking().ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Student>> FindAsync(Expression<Func<Student, bool>> predicate, CancellationToken cancellationToken = default)
        => await Set.AsNoTracking().Where(predicate).ToListAsync(cancellationToken);

    public async Task<Student?> FirstOrDefaultAsync(Expression<Func<Student, bool>> predicate, CancellationToken cancellationToken = default)
        => await Set.AsNoTracking().FirstOrDefaultAsync(predicate, cancellationToken);

    public async Task<bool> ExistsAsync(Expression<Func<Student, bool>> predicate, CancellationToken cancellationToken = default)
        => await Set.AnyAsync(predicate, cancellationToken);

    public async Task<int> CountAsync(Expression<Func<Student, bool>>? predicate = null, CancellationToken cancellationToken = default)
        => predicate is null
            ? await Set.CountAsync(cancellationToken)
            : await Set.CountAsync(predicate, cancellationToken);

    public async Task AddAsync(Student entity, CancellationToken cancellationToken = default)
        => await Set.AddAsync(entity, cancellationToken);

    public void Update(Student entity) => Set.Update(entity);

    public void Remove(Student entity) => Set.Remove(entity);
}
