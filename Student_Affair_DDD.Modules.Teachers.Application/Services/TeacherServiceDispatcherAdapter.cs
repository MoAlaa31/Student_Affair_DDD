
namespace Student_Affair_DDD.Modules.Teachers.Application.Services;

using Student_Affair_DDD.Modules.Teachers.Application.UseCases.CreateTeacher;
using Student_Affair_DDD.Modules.Teachers.Application.UseCases.GetTeacherById;
using Student_Affair_DDD.Modules.Teachers.Application.UseCases.GetTeachersPaged;
using Student_Affair_DDD.Modules.Teachers.Application.UseCases.UpdateTeacher;

public sealed class TeacherServiceDispatcherAdapter(ITeachersDispatcher dispatcher) : ITeacherService
{
    public Task<PagedTeachers> GetPagedAsync(TeacherPaginationSpecParams query, CancellationToken cancellationToken = default)
        => dispatcher.Send(new GetTeachersPagedQuery(query), cancellationToken);

    public Task<TeacherDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => dispatcher.Send(new GetTeacherByIdQuery(id), cancellationToken);

    public Task<TeacherDto> CreateAsync(TeacherCreateDto request, CancellationToken cancellationToken = default)
        => dispatcher.Send(new CreateTeacherCommand(request), cancellationToken);

    public Task<TeacherDto?> UpdateAsync(int id, TeacherUpdateDto request, CancellationToken cancellationToken = default)
        => dispatcher.Send(new UpdateTeacherCommand(id, request), cancellationToken);
}
