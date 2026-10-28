namespace Student_Affair_DDD.Modules.Classrooms.Shared.DTOs;

public sealed record ClassroomDto(
    int Id,
    string Code,
    string Building,
    string RoomNumber,
    int Capacity,
    bool IsActive,
    DateTime CreatedAtUtc,
    IReadOnlyList<int> StudentIds,
    IReadOnlyList<int> TeacherIds);
