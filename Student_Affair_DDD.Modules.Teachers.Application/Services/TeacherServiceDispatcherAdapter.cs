using Student_Affair_DDD.Modules.Teachers.Application.Common;
using Student_Affair_DDD.Modules.Teachers.Application.Messaging;
using Student_Affair_DDD.Modules.Teachers.Application.Teachers;
using Student_Affair_DDD.Modules.Teachers.Shared.DTOs;

namespace Student_Affair_DDD.Modules.Teachers.Application.Services;

public sealed class TeacherServiceDispatcherAdapter(ITeachersDispatcher dispatcher) : ITeacherService
{
    public Task<PagedTeachers> GetPagedAsync(TeacherPaginationSpecParams query, CancellationToken cancellationToken = default)
        => dispatcher.Send(new GetTeachers(query), cancellationToken);

    public Task<TeacherDto> CreateAsync(TeacherCreateDto request, CancellationToken cancellationToken = default)
        => dispatcher.Send(new RegisterTeacher(request), cancellationToken);
}
