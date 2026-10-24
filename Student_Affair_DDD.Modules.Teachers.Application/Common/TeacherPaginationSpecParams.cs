namespace Student_Affair_DDD.Modules.Teachers.Application.Common;

public sealed class TeacherPaginationSpecParams
{
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public string? Search { get; set; }
    public string? Department { get; set; }
    public bool? IsActive { get; set; }
    public string? SortBy { get; set; }
    public bool SortDescending { get; set; }
}
