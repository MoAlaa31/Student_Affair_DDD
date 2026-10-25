

namespace Student_Affair_DDD.Modules.Students.Application.UseCases.DeleteStudent
{
    public sealed record DeleteStudentCommand(int Id) : IRequest<bool>
    {
    }
}