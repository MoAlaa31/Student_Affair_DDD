
using Student_Affair_DDD.Modules.Students.Application.Common.Interfaces;
using Student_Affair_DDD.Modules.Students.Application.Common.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace Student_Affair_DDD.Modules.Students.Application.UseCases.GetDepartments
{
    public sealed class GetDepartmentsHandler : IHandler<GetDepartmentsQuery, IReadOnlyList<string>>
    {
        private readonly IUnitOfWork _uow;

        public GetDepartmentsHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<IReadOnlyList<string>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken = default)
        {
            return await _uow.Students.GetDepartmentsAsync(cancellationToken);
        }
    }
}
