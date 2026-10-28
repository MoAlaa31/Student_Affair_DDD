namespace Student_Affair_DDD.Modules.Classrooms.Shared.DTOs;

public sealed record PagedClassrooms(
    IReadOnlyList<ClassroomDto> Items,
    int TotalCount,
    int PageIndex,
    int PageSize);
