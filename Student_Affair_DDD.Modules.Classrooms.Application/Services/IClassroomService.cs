using Student_Affair_DDD.Modules.Classrooms.Application.Classrooms;
using Student_Affair_DDD.Modules.Classrooms.Application.Common;
using Student_Affair_DDD.Modules.Classrooms.Shared.DTOs;

namespace Student_Affair_DDD.Modules.Classrooms.Application.Services;

public interface IClassroomService
{
    Task<PagedClassrooms> GetPagedAsync(ClassroomPaginationSpecParams query, CancellationToken cancellationToken = default);
    Task<ClassroomDto> CreateAsync(ClassroomCreateDto request, CancellationToken cancellationToken = default);
    Task<ClassroomDto?> UpdateAsync(int id, ClassroomUpdateDto request, CancellationToken cancellationToken = default);
}
