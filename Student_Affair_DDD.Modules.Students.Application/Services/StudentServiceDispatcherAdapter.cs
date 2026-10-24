using Student_Affair_DDD.Modules.Students.Application.Common.Messaging;
using Student_Affair_DDD.Modules.Students.Application.UseCases.CreateStudent;
using Student_Affair_DDD.Modules.Students.Application.UseCases.DeleteStudent;
using Student_Affair_DDD.Modules.Students.Application.UseCases.GetAllStudents;
using Student_Affair_DDD.Modules.Students.Application.UseCases.GetDepartments;
using Student_Affair_DDD.Modules.Students.Application.UseCases.GetStudentById;
using Student_Affair_DDD.Modules.Students.Application.UseCases.GetStudentsPaged;
using Student_Affair_DDD.Modules.Students.Application.UseCases.UpdateStudent;
using Student_Affair_DDD.Modules.Students.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Student_Affair_DDD.Modules.Students.Application.Services
{
    public sealed class StudentServiceDispatcherAdapter : IStudentService
    {
        private readonly IDispatcher _dispatcher;
        public StudentServiceDispatcherAdapter(IDispatcher dispatcher) => _dispatcher = dispatcher;

        public Task<PagedResult<StudentDto>> GetPagedAsync(StudentQuery query, CancellationToken cancellationToken = default)
            => _dispatcher.Send(new GetStudentsPagedQuery(query), cancellationToken);

        public Task<IReadOnlyList<StudentDto>> GetAllAsync(CancellationToken cancellationToken = default)
            => _dispatcher.Send(new GetAllStudentsQuery(), cancellationToken);

        public Task<StudentDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
            => _dispatcher.Send(new GetStudentByIdQuery(id), cancellationToken);

        public Task<StudentDto> CreateAsync(StudentCreateDto dto, CancellationToken cancellationToken = default)
            => _dispatcher.Send(new CreateStudentCommand(dto), cancellationToken);

        public Task<StudentDto> UpdateAsync(int id, StudentUpdateDto dto, CancellationToken cancellationToken = default)
            => _dispatcher.Send(new UpdateStudentCommand(id, dto), cancellationToken);

        public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
            => _dispatcher.Send(new DeleteStudentCommand(id), cancellationToken);

        public Task<IReadOnlyList<string>> GetDepartmentsAsync(CancellationToken cancellationToken = default)
            => _dispatcher.Send(new GetDepartmentsQuery(), cancellationToken);
    }
}
