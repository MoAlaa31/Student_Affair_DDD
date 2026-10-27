namespace Student_Affair_DDD.Modules.Classrooms.Application.UseCases.GetClassroomsPaged;

public sealed record GetClassroomsPagedQuery(ClassroomPaginationSpecParams Spec) : IRequest<PagedClassrooms>;
