namespace Student_Affair_DDD.Web.Client.Services;

public sealed class ClassroomApi(HttpClient httpClient) : IClassroomApi
{
    public async Task<ClassroomDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        using var response = await httpClient.GetAsync($"api/classrooms/{id}", cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound) return null;
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ClassroomDto>(cancellationToken: cancellationToken);
    }

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

    public async Task<ClassroomDto?> UpdateAsync(int id, ClassroomUpdateDto dto, CancellationToken cancellationToken = default)
    {
        using var response = await httpClient.PutAsJsonAsync($"api/classrooms/{id}", dto, cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ClassroomDto>(cancellationToken: cancellationToken);
    }

    public async Task<ClassroomDto?> AddStudentsAsync(int id, ClassroomStudentIdsDto dto, CancellationToken cancellationToken = default)
    {
        using var response = await httpClient.PostAsJsonAsync($"api/classrooms/{id}/students", dto, cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound) return null;
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ClassroomDto>(cancellationToken: cancellationToken);
    }

    public async Task<ClassroomDto?> AddTeachersAsync(int id, ClassroomTeacherIdsDto dto, CancellationToken cancellationToken = default)
    {
        using var response = await httpClient.PostAsJsonAsync($"api/classrooms/{id}/teachers", dto, cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound) return null;
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ClassroomDto>(cancellationToken: cancellationToken);
    }

    public async Task<bool> RemoveStudentAsync(int id, int studentId, CancellationToken cancellationToken = default)
    {
        using var response = await httpClient.DeleteAsync($"api/classrooms/{id}/students/{studentId}", cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound) return false;
        response.EnsureSuccessStatusCode();
        return true;
    }

    public async Task<bool> RemoveTeacherAsync(int id, int teacherId, CancellationToken cancellationToken = default)
    {
        using var response = await httpClient.DeleteAsync($"api/classrooms/{id}/teachers/{teacherId}", cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound) return false;
        response.EnsureSuccessStatusCode();
        return true;
    }
}
