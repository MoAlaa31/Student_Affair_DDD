namespace Student_Affair_DDD.Modules.Classrooms.Application.Common.Interfaces;

public interface ITeacherDirectory
{
    Task<IReadOnlyList<int>> GetUnavailableTeacherIdsAsync(
        IReadOnlyCollection<int> teacherIds,
        CancellationToken cancellationToken = default);
}
