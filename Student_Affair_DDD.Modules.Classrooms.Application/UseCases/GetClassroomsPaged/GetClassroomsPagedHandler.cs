namespace Student_Affair_DDD.Modules.Classrooms.Application.UseCases.GetClassroomsPaged;

public sealed class GetClassroomsPagedHandler(IClassroomRepository repository)
    : IHandler<GetClassroomsPagedQuery, PagedClassrooms>
{
    public async Task<PagedClassrooms> Handle(GetClassroomsPagedQuery query, CancellationToken cancellationToken)
    {
        var (items, totalCount) = await repository.GetPagedAsync(query.Spec, cancellationToken);
        var results = items.Select(classroom => new ClassroomDto(classroom.Id, classroom.Code,
            classroom.Building, classroom.RoomNumber, classroom.Capacity, classroom.IsActive,
            classroom.CreatedAtUtc)).ToArray();
        return new PagedClassrooms(results, totalCount, query.Spec.PageIndex, query.Spec.PageSize);
    }
}
