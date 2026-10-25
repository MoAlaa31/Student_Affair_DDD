
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Student_Affair_DDD.Modules.Students.Application.UseCases.UpdateStudent
{
    public sealed class UpdateStudentHandler : IHandler<UpdateStudentCommand, StudentDto>
    {
        private readonly IUnitOfWork _uow;

        public UpdateStudentHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<StudentDto> Handle(UpdateStudentCommand request, CancellationToken cancellationToken = default)
        {
            var dto = request.Dto;
            var existing = await _uow.Students.GetByIdAsync(dto.Id, cancellationToken);
            if (existing is null)
                throw new InvalidOperationException("Student not found.");

            if (await _uow.Students.EmailExistsAsync(dto.Email, existing.Id, cancellationToken))
                throw new InvalidOperationException("Email already exists.");

            dto.Apply(existing);
            _uow.Students.Update(existing);
            await _uow.SaveChangesAsync(cancellationToken);

            return existing.ToDto();
        }
    }
}
