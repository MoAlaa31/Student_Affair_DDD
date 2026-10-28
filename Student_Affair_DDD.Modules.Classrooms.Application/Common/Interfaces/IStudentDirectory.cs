namespace Student_Affair_DDD.Modules.Classrooms.Application.Common.Interfaces;

public interface IStudentDirectory
{
    Task<IReadOnlyList<int>> GetUnavailableStudentIdsAsync(
        IReadOnlyCollection<int> studentIds,
        CancellationToken cancellationToken = default);
}
