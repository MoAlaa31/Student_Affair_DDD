using Student_Affair_DDD.Modules.Students.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Student_Affair_DDD.Modules.Students.Application.Services
{
    public interface IStudentService
    {
        Task<PagedResult<StudentDto>> GetPagedAsync(StudentQuery query, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<StudentDto>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<StudentDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<StudentDto> CreateAsync(StudentCreateDto dto, CancellationToken cancellationToken = default);

        Task<StudentDto> UpdateAsync(int id, StudentUpdateDto dto, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<string>> GetDepartmentsAsync(CancellationToken cancellationToken = default);
    }
}
