using Student_Affair_DDD.Modules.Classrooms.Application.Messaging;
using Student_Affair_DDD.Modules.Classrooms.Domain;
using Student_Affair_DDD.Modules.Classrooms.Application.Common;
using Student_Affair_DDD.Modules.Classrooms.Application.Common.Interfaces;
using Student_Affair_DDD.Modules.Classrooms.Application.Common.Interfaces.EntitiesIRepos;
using Student_Affair_DDD.Modules.Classrooms.Shared.DTOs;

namespace Student_Affair_DDD.Modules.Classrooms.Application.Classrooms;

public sealed record RegisterClassroom(ClassroomCreateDto Request) : IRequest<ClassroomDto>;
public sealed record UpdateClassroom(int Id, ClassroomUpdateDto Request) : IRequest<ClassroomDto?>;
public sealed record GetClassrooms(ClassroomPaginationSpecParams Spec) : IRequest<PagedClassrooms>;

public sealed class RegisterClassroomHandler(IUnitOfWork unitOfWork) : IHandler<RegisterClassroom, ClassroomDto>
{
    public async Task<ClassroomDto> Handle(RegisterClassroom command, CancellationToken cancellationToken)
    {
        var request = command.Request;
        if (await unitOfWork.Classrooms.CodeExistsAsync(request.Code, cancellationToken: cancellationToken))
            throw new InvalidOperationException("A classroom with that code already exists.");
        var classroom = Classroom.Create(request.Code, request.Building, request.RoomNumber, request.Capacity);
        await unitOfWork.Classrooms.AddAsync(classroom, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Map(classroom);
    }

    private static ClassroomDto Map(Classroom c) => new(c.Id, c.Code, c.Building, c.RoomNumber, c.Capacity, c.IsActive, c.CreatedAtUtc);
}

public sealed class UpdateClassroomHandler(IUnitOfWork unitOfWork) : IHandler<UpdateClassroom, ClassroomDto?>
{
    public async Task<ClassroomDto?> Handle(UpdateClassroom command, CancellationToken cancellationToken)
    {
        var classroom = await unitOfWork.Classrooms.GetByIdAsync(command.Id, cancellationToken);
        if (classroom is null)
            return null;

        var request = command.Request;
        if (await unitOfWork.Classrooms.CodeExistsAsync(request.Code, command.Id, cancellationToken))
            throw new InvalidOperationException("A classroom with that code already exists.");

        classroom.UpdateDetails(request.Code, request.Building, request.RoomNumber, request.Capacity);
        unitOfWork.Classrooms.Update(classroom);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new ClassroomDto(classroom.Id, classroom.Code, classroom.Building, classroom.RoomNumber,
            classroom.Capacity, classroom.IsActive, classroom.CreatedAtUtc);
    }
}

public sealed class GetClassroomsHandler(IClassroomRepository repository) : IHandler<GetClassrooms, PagedClassrooms>
{
    public async Task<PagedClassrooms> Handle(GetClassrooms query, CancellationToken cancellationToken)
    {
        var (items, totalCount) = await repository.GetPagedAsync(query.Spec, cancellationToken);
        var responses = items.Select(c => new ClassroomDto(c.Id, c.Code, c.Building, c.RoomNumber, c.Capacity, c.IsActive, c.CreatedAtUtc)).ToArray();
        return new PagedClassrooms(responses, totalCount, query.Spec.PageIndex, query.Spec.PageSize);
    }
}
