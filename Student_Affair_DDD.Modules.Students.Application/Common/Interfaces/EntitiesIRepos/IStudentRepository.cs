using Student_Affair_DDD.Modules.Students.Application.SpecParams;
using Student_Affair_DDD.Modules.Students.Domain.Entities;
using System.Linq.Expressions;

namespace Student_Affair_DDD.Modules.Students.Application.Common.Interfaces.EntitiesIRepos;

public interface IStudentRepository
{
    Task<bool> EmailExistsAsync(string email, int? excludeId = null, CancellationToken cancellationToken = default);
    Task<bool> StudentNumberExistsAsync(string number, int? excludeId = null, CancellationToken cancellationToken = default);
    Task<bool> NationalIdExistsAsync(string id, int? excludeId = null, CancellationToken cancellationToken = default);
    Task<(IReadOnlyList<Student> Items, int TotalCount)> GetPagedAsync(
        StudentPaginationSpecParams spec,
        CancellationToken cancellationToken = default);
    Task<IReadOnlyList<string>> GetDepartmentsAsync(CancellationToken cancellationToken = default);
    Task<Student?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Student>> ListAllAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Student>> FindAsync(Expression<Func<Student, bool>> expression, CancellationToken cancellationToken = default);

    Task<Student?> FirstOrDefaultAsync(Expression<Func<Student, bool>> expression, CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(Expression<Func<Student, bool>> expression, CancellationToken cancellationToken = default);

    Task<int> CountAsync(Expression<Func<Student, bool>>? expression = null, CancellationToken cancellationToken = default);

    Task AddAsync(Student entity, CancellationToken cancellationToken = default);

    void Update(Student entity);

    void Remove(Student entity);
    Task SaveChanges(CancellationToken cancellationToken);
}
