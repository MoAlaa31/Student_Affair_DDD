
using Student_Affair_DDD.Modules.Classrooms.Application.Common.Messaging;
using Student_Affair_DDD.Modules.Classrooms.Application.SpecParams;
using Student_Affair_DDD.Modules.Classrooms.Application.UseCases.CreateClassroom;
using Student_Affair_DDD.Modules.Classrooms.Application.UseCases.GetClassroomsPaged;
using Student_Affair_DDD.Modules.Classrooms.Application.UseCases.UpdateClassroom;

namespace Student_Affair_DDD.Modules.Classrooms.Application.Services;

public sealed class ClassroomServiceDispatcherAdapter(IDispatcher dispatcher) : IClassroomService
{
    public Task<PagedClassrooms> GetPagedAsync(ClassroomPaginationSpecParams query, CancellationToken cancellationToken = default)
        => dispatcher.Send(new GetClassroomsPagedQuery(query), cancellationToken);

    public Task<ClassroomDto> CreateAsync(ClassroomCreateDto request, CancellationToken cancellationToken = default)
        => dispatcher.Send(new CreateClassroomCommand(request), cancellationToken);

    public Task<ClassroomDto?> UpdateAsync(int id, ClassroomUpdateDto request, CancellationToken cancellationToken = default)
        => dispatcher.Send(new UpdateClassroomCommand(id, request), cancellationToken);
}
