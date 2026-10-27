
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Affair_DDD.Modules.Students.Application.UseCases.GetStudentsPaged
{
    public sealed class GetStudentsPagedHandler : IHandler<GetStudentsPagedQuery, PagedResult<StudentDto>>
    {
        private readonly IUnitOfWork _uow;

        public GetStudentsPagedHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<PagedResult<StudentDto>> Handle(GetStudentsPagedQuery request, CancellationToken cancellationToken = default)
        {
            var spec = new StudentPaginationSpecParams();
            spec.Search = request.Query.Search;
            spec.department = request.Query.Department;
            spec.IsActive = request.Query.IsActive;
            spec.SortBy = request.Query.SortBy;
            spec.sortDescending = request.Query.SortDescending;
            spec.PageIndex = request.Query.Page < 1 ? 1 : request.Query.Page;
            spec.PageSize = request.Query.PageSize is < 1 or > 200 ? 10 : request.Query.PageSize;

            var (items, total) = await _uow.Students.GetPagedAsync(
                spec, cancellationToken);

            return new PagedResult<StudentDto>(items.Select(s => s.ToDto()).ToList(), total, spec.PageIndex, spec.PageSize);
        }
    }
}
