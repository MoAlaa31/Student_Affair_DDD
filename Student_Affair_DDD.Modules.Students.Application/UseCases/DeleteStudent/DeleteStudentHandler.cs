
using Student_Affair_DDD.Modules.Students.Application.Common.Interfaces;
using Student_Affair_DDD.Modules.Students.Application.Common.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace Student_Affair_DDD.Modules.Students.Application.UseCases.DeleteStudent
{
    public sealed class DeleteStudentHandler : IHandler<DeleteStudentCommand, bool>
    {
        private readonly IUnitOfWork _uow;

        public DeleteStudentHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<bool> Handle(DeleteStudentCommand request, CancellationToken cancellationToken = default)
        {
            var existing = await _uow.Students.GetByIdAsync(request.Id, cancellationToken);
            if (existing is null) return false;

            _uow.Students.Remove(existing);
            await _uow.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
