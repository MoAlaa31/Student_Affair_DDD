using Student_Affair_DDD.Modules.Teachers.Shared.DTOs;

namespace Student_Affair_DDD.Modules.Teachers.Shared.Abstractions;

public interface ITeacherApi
{
    Task<PagedTeachers> GetPagedAsync(TeacherQuery query, CancellationToken cancellationToken = default);
    Task<TeacherDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<TeacherDto> CreateAsync(TeacherCreateDto dto, CancellationToken cancellationToken = default);
    Task<TeacherDto?> UpdateAsync(int id, TeacherUpdateDto dto, CancellationToken cancellationToken = default);
}
