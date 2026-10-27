
using Student_Affair_DDD.Modules.Classrooms.Application.Common.Messaging;
using Student_Affair_DDD.Modules.Classrooms.Application.SpecParams;

namespace Student_Affair_DDD.Modules.Classrooms.Application.Services;

public sealed class ClassroomServiceDispatcherAdapter(IDispatcher dispatcher) : IClassroomService
{
    public Task<PagedClassrooms> GetPagedAsync(ClassroomPaginationSpecParams query, CancellationToken cancellationToken = default)
        => dispatcher.Send(new GetClassrooms(query), cancellationToken);

    public Task<ClassroomDto> CreateAsync(ClassroomCreateDto request, CancellationToken cancellationToken = default)
        => dispatcher.Send(new RegisterClassroom(request), cancellationToken);

    public Task<ClassroomDto?> UpdateAsync(int id, ClassroomUpdateDto request, CancellationToken cancellationToken = default)
        => dispatcher.Send(new UpdateClassroom(id, request), cancellationToken);
}
