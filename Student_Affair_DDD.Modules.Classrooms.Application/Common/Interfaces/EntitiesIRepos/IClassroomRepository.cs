
using Student_Affair_DDD.Modules.Classrooms.Application.SpecParams;

namespace Student_Affair_DDD.Modules.Classrooms.Application.Common.Interfaces.EntitiesIRepos;

public interface IClassroomRepository
{
    Task<Classroom?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> CodeExistsAsync(string code, int? excludeId = null, CancellationToken cancellationToken = default);
    Task<(IReadOnlyList<Classroom> Items, int TotalCount)> GetPagedAsync(ClassroomPaginationSpecParams spec, CancellationToken cancellationToken = default);
    Task AddAsync(Classroom classroom, CancellationToken cancellationToken = default);
    void Update(Classroom classroom);
    void Remove(Classroom classroom);
}
