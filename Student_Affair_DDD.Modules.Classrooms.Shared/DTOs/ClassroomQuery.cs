namespace Student_Affair_DDD.Modules.Classrooms.Shared.DTOs;

public sealed class ClassroomQuery
{
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public string? Search { get; set; }
    public bool? IsActive { get; set; }
    public string? SortBy { get; set; }
    public bool SortDescending { get; set; }
}
