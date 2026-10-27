namespace Student_Affair_DDD.Modules.Teachers.Application.UseCases.GetTeachersPaged;

public sealed record GetTeachersPagedQuery(TeacherPaginationSpecParams Spec) : IRequest<PagedTeachers>;
