using Student_Affair_DDD.Modules.Classrooms.Application.Classrooms;
using Student_Affair_DDD.Modules.Classrooms.Application.Common;
using Student_Affair_DDD.Modules.Classrooms.Application.Messaging;
using Student_Affair_DDD.Modules.Classrooms.Shared.DTOs;

namespace Student_Affair_DDD.Modules.Classrooms.Application.Services;

public sealed class ClassroomServiceDispatcherAdapter(IClassroomsDispatcher dispatcher) : IClassroomService
{
    public Task<PagedClassrooms> GetPagedAsync(ClassroomPaginationSpecParams query, CancellationToken cancellationToken = default)
        => dispatcher.Send(new GetClassrooms(query), cancellationToken);

    public Task<ClassroomDto> CreateAsync(ClassroomCreateDto request, CancellationToken cancellationToken = default)
        => dispatcher.Send(new RegisterClassroom(request), cancellationToken);
}
