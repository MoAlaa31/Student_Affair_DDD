namespace Student_Affair_DDD.Modules.Teachers.Shared.DTOs;

public sealed record PagedTeachers(
    IReadOnlyList<TeacherDto> Items,
    int TotalCount,
    int PageIndex,
    int PageSize);
