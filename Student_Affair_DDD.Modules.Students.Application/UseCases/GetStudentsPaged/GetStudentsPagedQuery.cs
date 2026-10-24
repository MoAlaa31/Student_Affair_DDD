
using Student_Affair_DDD.Modules.Students.Application.Common.Messaging;
using Student_Affair_DDD.Modules.Students.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Affair_DDD.Modules.Students.Application.UseCases.GetStudentsPaged
{
    public sealed record GetStudentsPagedQuery(StudentQuery Query) : IRequest<PagedResult<StudentDto>>;
}
