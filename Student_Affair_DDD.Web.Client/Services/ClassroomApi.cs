using System.Net.Http.Json;
using Student_Affair_DDD.Modules.Classrooms.Shared.Abstractions;
using Student_Affair_DDD.Modules.Classrooms.Shared.DTOs;

namespace Student_Affair_DDD.Web.Client.Services;

public sealed class ClassroomApi(HttpClient httpClient) : IClassroomApi
{
    public async Task<PagedClassrooms> GetPagedAsync(ClassroomQuery query, CancellationToken cancellationToken = default)
    {
        var url = $"api/classrooms?pageIndex={query.PageIndex}&pageSize={query.PageSize}&search={Uri.EscapeDataString(query.Search ?? "")}&isActive={query.IsActive}&sortBy={Uri.EscapeDataString(query.SortBy ?? "")}&sortDescending={query.SortDescending}";
        return await httpClient.GetFromJsonAsync<PagedClassrooms>(url, cancellationToken)
            ?? new PagedClassrooms([], 0, query.PageIndex, query.PageSize);
    }

    public async Task<ClassroomDto> CreateAsync(ClassroomCreateDto dto, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("api/classrooms", dto, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ClassroomDto>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("The API returned an empty classroom response.");
    }
}
