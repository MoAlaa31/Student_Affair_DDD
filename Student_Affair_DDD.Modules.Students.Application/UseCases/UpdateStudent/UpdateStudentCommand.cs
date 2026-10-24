

using Student_Affair_DDD.Modules.Students.Application.Common.Messaging;
using Student_Affair_DDD.Modules.Students.Shared.DTOs;

namespace Student_Affair_DDD.Modules.Students.Application.UseCases.UpdateStudent
{
    public sealed record UpdateStudentCommand(int Id, StudentUpdateDto Dto) : IRequest<StudentDto>
    {
    }
}