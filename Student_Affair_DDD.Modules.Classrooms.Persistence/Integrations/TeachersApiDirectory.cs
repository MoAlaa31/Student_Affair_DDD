using System.Net;
using System.Net.Http.Json;
using Student_Affair_DDD.Modules.Classrooms.Application.Common.Interfaces;

namespace Student_Affair_DDD.Modules.Classrooms.Persistence.Integrations;

internal sealed class TeachersApiDirectory(HttpClient httpClient) : ITeacherDirectory
{
    public async Task<IReadOnlyList<int>> GetUnavailableTeacherIdsAsync(
        IReadOnlyCollection<int> teacherIds,
        CancellationToken cancellationToken = default)
    {
        var checks = teacherIds.Distinct().Select(async id =>
            (Id: id, IsAvailable: await IsActiveAsync(id, cancellationToken)));
        var results = await Task.WhenAll(checks);
        return results.Where(result => !result.IsAvailable).Select(result => result.Id).ToArray();
    }

    private async Task<bool> IsActiveAsync(int id, CancellationToken cancellationToken)
    {
        using var response = await httpClient.GetAsync($"api/teachers/{id}", cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound || response.StatusCode == HttpStatusCode.Gone)
            return false;

        response.EnsureSuccessStatusCode();
        var teacher = await response.Content.ReadFromJsonAsync<PersonStatus>(cancellationToken: cancellationToken);
        return teacher?.IsActive == true;
    }

    private sealed record PersonStatus(bool IsActive);
}
