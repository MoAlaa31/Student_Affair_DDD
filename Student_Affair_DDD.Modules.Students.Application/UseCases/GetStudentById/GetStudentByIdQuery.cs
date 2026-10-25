
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Affair_DDD.Modules.Students.Application.UseCases.GetStudentById
{
    public sealed record GetStudentByIdQuery(int Id) : IRequest<StudentDto?>;
}
