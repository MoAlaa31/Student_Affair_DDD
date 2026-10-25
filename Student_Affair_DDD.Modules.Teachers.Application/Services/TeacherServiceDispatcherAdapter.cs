
namespace Student_Affair_DDD.Modules.Teachers.Application.Services;

public sealed class TeacherServiceDispatcherAdapter(ITeachersDispatcher dispatcher) : ITeacherService
{
    public Task<PagedTeachers> GetPagedAsync(TeacherPaginationSpecParams query, CancellationToken cancellationToken = default)
        => dispatcher.Send(new GetTeachers(query), cancellationToken);

    public Task<TeacherDto> CreateAsync(TeacherCreateDto request, CancellationToken cancellationToken = default)
        => dispatcher.Send(new RegisterTeacher(request), cancellationToken);

    public Task<TeacherDto?> UpdateAsync(int id, TeacherUpdateDto request, CancellationToken cancellationToken = default)
        => dispatcher.Send(new UpdateTeacher(id, request), cancellationToken);
}
