using Student_Affair_DDD.Modules.Students.Application.Common.Exceptions;
using Student_Affair_DDD.Modules.Students.Application.Common.Interfaces;
using Student_Affair_DDD.Modules.Students.Application.Common.Mapping;
using Student_Affair_DDD.Modules.Students.Application.Common.Messaging;
using Student_Affair_DDD.Modules.Students.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Affair_DDD.Modules.Students.Application.UseCases.CreateStudent
{
    public sealed class CreateStudentHandler : IHandler<CreateStudentCommand, StudentDto>
    {
        private readonly IUnitOfWork _uow;

        public CreateStudentHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<StudentDto> Handle(CreateStudentCommand request, CancellationToken cancellationToken = default)
        {
            var dto = request.Dto;

            if (await _uow.Students.EmailExistsAsync(dto.Email, null, cancellationToken))
                throw new BusinessRuleException($"A student with e-mail '{dto.Email}' already exists.");

            if (await _uow.Students.StudentNumberExistsAsync(dto.StudentNumber, null, cancellationToken))
                throw new BusinessRuleException($"Student number '{dto.StudentNumber}' is already in use.");

            if (await _uow.Students.NationalIdExistsAsync(dto.NationalId, null, cancellationToken))
                throw new BusinessRuleException($"National id '{dto.NationalId}' is already registered.");

            var entity = dto.ToEntity();
            await _uow.Students.AddAsync(entity, cancellationToken);
            await _uow.SaveChangesAsync(cancellationToken);

            //_logger.LogInformation("Created student {StudentId} ({StudentNumber})", entity.Id, entity.StudentNumber);
            return entity.ToDto();
        }
    }
}
