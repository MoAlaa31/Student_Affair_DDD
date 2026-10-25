namespace Student_Affair_DDD.Web.Client.Services;

public sealed class TeacherApi(HttpClient httpClient) : ITeacherApi
{
    public async Task<PagedTeachers> GetPagedAsync(TeacherQuery query, CancellationToken cancellationToken = default)
    {
        var url = $"api/teachers?pageIndex={query.PageIndex}&pageSize={query.PageSize}&search={Uri.EscapeDataString(query.Search ?? "")}&department={Uri.EscapeDataString(query.Department ?? "")}&isActive={query.IsActive}&sortBy={Uri.EscapeDataString(query.SortBy ?? "")}&sortDescending={query.SortDescending}";
        return await httpClient.GetFromJsonAsync<PagedTeachers>(url, cancellationToken)
            ?? new PagedTeachers([], 0, query.PageIndex, query.PageSize);
    }

    public async Task<TeacherDto> CreateAsync(TeacherCreateDto dto, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("api/teachers", dto, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TeacherDto>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("The API returned an empty teacher response.");
    }

    public async Task<TeacherDto?> UpdateAsync(int id, TeacherUpdateDto dto, CancellationToken cancellationToken = default)
    {
        using var response = await httpClient.PutAsJsonAsync($"api/teachers/{id}", dto, cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TeacherDto>(cancellationToken: cancellationToken);
    }
}
