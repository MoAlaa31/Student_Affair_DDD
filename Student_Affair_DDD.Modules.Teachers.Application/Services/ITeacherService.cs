
namespace Student_Affair_DDD.Modules.Teachers.Application.Services;

public interface ITeacherService
{
    Task<PagedTeachers> GetPagedAsync(TeacherPaginationSpecParams query, CancellationToken cancellationToken = default);
    Task<TeacherDto> CreateAsync(TeacherCreateDto request, CancellationToken cancellationToken = default);
    Task<TeacherDto?> UpdateAsync(int id, TeacherUpdateDto request, CancellationToken cancellationToken = default);
}
