namespace Student_Affair_DDD.Modules.Classrooms.Application.SpecParams;

public sealed class ClassroomPaginationSpecParams : PaginationSpecParams
{
    public bool? IsActive { get; set; }
    public string? SortBy { get; set; }
    public bool SortDescending { get; set; }
}
