
using Student_Affair_DDD.Modules.Students.Application.Common.Interfaces;
using Student_Affair_DDD.Modules.Students.Application.Common.Mapping;
using Student_Affair_DDD.Modules.Students.Application.Common.Messaging;
using Student_Affair_DDD.Modules.Students.Shared.DTOs;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Student_Affair_DDD.Modules.Students.Application.UseCases.GetAllStudents
{
    public sealed class GetAllStudentsHandler : IHandler<GetAllStudentsQuery, IReadOnlyList<StudentDto>>
    {
        private readonly IUnitOfWork _uow;

        public GetAllStudentsHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<IReadOnlyList<StudentDto>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken = default)
        {
            var items = await _uow.Students.ListAllAsync(cancellationToken);
            return items.Select(s => s.ToDto()).ToList();
        }
    }
}
