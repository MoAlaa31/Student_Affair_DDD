using Student_Affair_DDD.Modules.Classrooms.Shared.DTOs;

namespace Student_Affair_DDD.Modules.Classrooms.Shared.Abstractions;

public interface IClassroomApi
{
    Task<PagedClassrooms> GetPagedAsync(ClassroomQuery query, CancellationToken cancellationToken = default);
    Task<ClassroomDto> CreateAsync(ClassroomCreateDto dto, CancellationToken cancellationToken = default);
    Task<ClassroomDto?> UpdateAsync(int id, ClassroomUpdateDto dto, CancellationToken cancellationToken = default);
}
