using Student_Affair_DDD.Modules.Teachers.Domain;

namespace Student_Affair_DDD.Modules.Teachers.Application.Common.Interfaces.EntitiesIRepos;

public interface ITeacherRepository
{
    Task<Teacher?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> EmployeeNumberExistsAsync(string employeeNumber, int? excludeId = null, CancellationToken cancellationToken = default);
    Task<bool> EmailExistsAsync(string email, int? excludeId = null, CancellationToken cancellationToken = default);
    Task<(IReadOnlyList<Teacher> Items, int TotalCount)> GetPagedAsync(TeacherPaginationSpecParams spec, CancellationToken cancellationToken = default);
    Task AddAsync(Teacher teacher, CancellationToken cancellationToken = default);
    void Update(Teacher teacher);
    void Remove(Teacher teacher);
}
