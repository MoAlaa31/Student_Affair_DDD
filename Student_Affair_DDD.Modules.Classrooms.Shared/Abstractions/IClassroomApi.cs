using Student_Affair_DDD.Modules.Classrooms.Shared.DTOs;

namespace Student_Affair_DDD.Modules.Classrooms.Shared.Abstractions;

public interface IClassroomApi
{
    Task<PagedClassrooms> GetPagedAsync(ClassroomQuery query, CancellationToken cancellationToken = default);
    Task<ClassroomDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<ClassroomDto> CreateAsync(ClassroomCreateDto dto, CancellationToken cancellationToken = default);
    Task<ClassroomDto?> UpdateAsync(int id, ClassroomUpdateDto dto, CancellationToken cancellationToken = default);
    Task<ClassroomDto?> AddStudentsAsync(int id, ClassroomStudentIdsDto dto, CancellationToken cancellationToken = default);
    Task<ClassroomDto?> AddTeachersAsync(int id, ClassroomTeacherIdsDto dto, CancellationToken cancellationToken = default);
    Task<bool> RemoveStudentAsync(int id, int studentId, CancellationToken cancellationToken = default);
    Task<bool> RemoveTeacherAsync(int id, int teacherId, CancellationToken cancellationToken = default);
}
