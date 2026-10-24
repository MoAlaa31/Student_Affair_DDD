using Student_Affair_DDD.Modules.Teachers.Application.Common;
using Student_Affair_DDD.Modules.Teachers.Application.Teachers;
using Student_Affair_DDD.Modules.Teachers.Shared.DTOs;

namespace Student_Affair_DDD.Modules.Teachers.Application.Services;

public interface ITeacherService
{
    Task<PagedTeachers> GetPagedAsync(TeacherPaginationSpecParams query, CancellationToken cancellationToken = default);
    Task<TeacherDto> CreateAsync(TeacherCreateDto request, CancellationToken cancellationToken = default);
}
