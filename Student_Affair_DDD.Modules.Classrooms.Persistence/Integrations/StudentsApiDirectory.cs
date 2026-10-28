using System.Net;
using System.Net.Http.Json;
using Student_Affair_DDD.Modules.Classrooms.Application.Common.Interfaces;

namespace Student_Affair_DDD.Modules.Classrooms.Persistence.Integrations;

internal sealed class StudentsApiDirectory(HttpClient httpClient) : IStudentDirectory
{
    public async Task<IReadOnlyList<int>> GetUnavailableStudentIdsAsync(
        IReadOnlyCollection<int> studentIds,
        CancellationToken cancellationToken = default)
    {
        var checks = studentIds.Distinct().Select(async id =>
            (Id: id, IsAvailable: await IsActiveAsync(id, cancellationToken)));
        var results = await Task.WhenAll(checks);
        return results.Where(result => !result.IsAvailable).Select(result => result.Id).ToArray();
    }

    private async Task<bool> IsActiveAsync(int id, CancellationToken cancellationToken)
    {
        using var response = await httpClient.GetAsync($"api/students/{id}", cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound || response.StatusCode == HttpStatusCode.Gone)
            return false;

        response.EnsureSuccessStatusCode();
        var student = await response.Content.ReadFromJsonAsync<PersonStatus>(cancellationToken: cancellationToken);
        return student?.IsActive == true;
    }

    private sealed record PersonStatus(bool IsActive);
}
