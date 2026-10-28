
using Student_Affair_DDD.Modules.Classrooms.Application.Common.Messaging;
using Student_Affair_DDD.Modules.Classrooms.Application.SpecParams;
using Student_Affair_DDD.Modules.Classrooms.Application.UseCases.CreateClassroom;
using Student_Affair_DDD.Modules.Classrooms.Application.UseCases.GetClassroomsPaged;
using Student_Affair_DDD.Modules.Classrooms.Application.UseCases.GetClassroomById;
using Student_Affair_DDD.Modules.Classrooms.Application.UseCases.AddStudentsToClassroom;
using Student_Affair_DDD.Modules.Classrooms.Application.UseCases.AddTeachersToClassroom;
using Student_Affair_DDD.Modules.Classrooms.Application.UseCases.RemoveStudentFromClassroom;
using Student_Affair_DDD.Modules.Classrooms.Application.UseCases.RemoveTeacherFromClassroom;
using Student_Affair_DDD.Modules.Classrooms.Application.UseCases.UpdateClassroom;

namespace Student_Affair_DDD.Modules.Classrooms.Application.Services;

public sealed class ClassroomServiceDispatcherAdapter(IDispatcher dispatcher) : IClassroomService
{
    public Task<PagedClassrooms> GetPagedAsync(ClassroomPaginationSpecParams query, CancellationToken cancellationToken = default)
        => dispatcher.Send(new GetClassroomsPagedQuery(query), cancellationToken);

    public Task<ClassroomDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => dispatcher.Send(new GetClassroomByIdQuery(id), cancellationToken);

    public Task<ClassroomDto> CreateAsync(ClassroomCreateDto request, CancellationToken cancellationToken = default)
        => dispatcher.Send(new CreateClassroomCommand(request), cancellationToken);

    public Task<ClassroomDto?> UpdateAsync(int id, ClassroomUpdateDto request, CancellationToken cancellationToken = default)
        => dispatcher.Send(new UpdateClassroomCommand(id, request), cancellationToken);

    public Task<ClassroomDto?> AddStudentsAsync(int id, ClassroomStudentIdsDto request, CancellationToken cancellationToken = default)
        => dispatcher.Send(new AddStudentsToClassroomCommand(id, request.StudentIds), cancellationToken);

    public Task<ClassroomDto?> AddTeachersAsync(int id, ClassroomTeacherIdsDto request, CancellationToken cancellationToken = default)
        => dispatcher.Send(new AddTeachersToClassroomCommand(id, request.TeacherIds), cancellationToken);

    public Task<bool> RemoveStudentAsync(int id, int studentId, CancellationToken cancellationToken = default)
        => dispatcher.Send(new RemoveStudentFromClassroomCommand(id, studentId), cancellationToken);

    public Task<bool> RemoveTeacherAsync(int id, int teacherId, CancellationToken cancellationToken = default)
        => dispatcher.Send(new RemoveTeacherFromClassroomCommand(id, teacherId), cancellationToken);
}
