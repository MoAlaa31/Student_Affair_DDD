
using Student_Affair_DDD.Modules.Students.Application.Common.Interfaces;
using Student_Affair_DDD.Modules.Students.Application.Common.Mapping;
using Student_Affair_DDD.Modules.Students.Application.Common.Messaging;
using Student_Affair_DDD.Modules.Students.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Affair_DDD.Modules.Students.Application.UseCases.GetStudentById
{
    public sealed class GetStudentByIdHandler : IHandler<GetStudentByIdQuery, StudentDto?>
    {
        private readonly IUnitOfWork _uow;
        //private readonly ILogger<GetStudentByIdHandler> _logger;

        public GetStudentByIdHandler(IUnitOfWork uow)
        {
            _uow = uow;
            //_logger = logger;
        }

        public async Task<StudentDto?> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken = default)
        {
            var entity = await _uow.Students.GetByIdAsync(request.Id, cancellationToken);
            return entity?.ToDto();
        }
    }
}
