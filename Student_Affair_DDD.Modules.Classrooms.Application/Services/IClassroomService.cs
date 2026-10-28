
using Student_Affair_DDD.Modules.Classrooms.Application.SpecParams;

namespace Student_Affair_DDD.Modules.Classrooms.Application.Services;

public interface IClassroomService
{
    Task<PagedClassrooms> GetPagedAsync(ClassroomPaginationSpecParams query, CancellationToken cancellationToken = default);
    Task<ClassroomDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<ClassroomDto> CreateAsync(ClassroomCreateDto request, CancellationToken cancellationToken = default);
    Task<ClassroomDto?> UpdateAsync(int id, ClassroomUpdateDto request, CancellationToken cancellationToken = default);
    Task<ClassroomDto?> AddStudentsAsync(int id, ClassroomStudentIdsDto request, CancellationToken cancellationToken = default);
    Task<ClassroomDto?> AddTeachersAsync(int id, ClassroomTeacherIdsDto request, CancellationToken cancellationToken = default);
    Task<bool> RemoveStudentAsync(int id, int studentId, CancellationToken cancellationToken = default);
    Task<bool> RemoveTeacherAsync(int id, int teacherId, CancellationToken cancellationToken = default);
}
