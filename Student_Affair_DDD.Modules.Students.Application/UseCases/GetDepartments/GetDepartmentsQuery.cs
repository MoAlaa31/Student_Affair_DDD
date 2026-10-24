
using Student_Affair_DDD.Modules.Students.Application.Common.Messaging;

namespace Student_Affair_DDD.Modules.Students.Application.UseCases.GetDepartments
{
    public sealed record GetDepartmentsQuery() : IRequest<IReadOnlyList<string>>
    {
    }
}