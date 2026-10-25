
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Affair_DDD.Modules.Students.Application.UseCases.CreateStudent
{
    public sealed record CreateStudentCommand(StudentCreateDto Dto) : IRequest<StudentDto>;
}
