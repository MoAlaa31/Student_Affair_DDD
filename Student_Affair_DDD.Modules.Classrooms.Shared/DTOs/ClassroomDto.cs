using System.ComponentModel.DataAnnotations;

namespace Student_Affair_DDD.Modules.Classrooms.Shared.DTOs;

public sealed record ClassroomDto(int Id, string Code, string Building, string RoomNumber, int Capacity, bool IsActive, DateTime CreatedAtUtc);

public sealed class ClassroomCreateDto
{
    [Required, StringLength(20, MinimumLength = 1)]
    public string Code { get; set; } = "";
    [Required, StringLength(100, MinimumLength = 1)]
    public string Building { get; set; } = "";
    [Required, StringLength(20, MinimumLength = 1)]
    public string RoomNumber { get; set; } = "";
    [Range(1, 1000)]
    public int Capacity { get; set; }
}

public sealed class ClassroomUpdateDto
{
    [Required, StringLength(20, MinimumLength = 1)]
    public string Code { get; set; } = "";
    [Required, StringLength(100, MinimumLength = 1)]
    public string Building { get; set; } = "";
    [Required, StringLength(20, MinimumLength = 1)]
    public string RoomNumber { get; set; } = "";
    [Range(1, 1000)]
    public int Capacity { get; set; }
}

public sealed class ClassroomQuery
{
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public string? Search { get; set; }
    public bool? IsActive { get; set; }
    public string? SortBy { get; set; }
    public bool SortDescending { get; set; }
}

public sealed record PagedClassrooms(IReadOnlyList<ClassroomDto> Items, int TotalCount, int PageIndex, int PageSize);
