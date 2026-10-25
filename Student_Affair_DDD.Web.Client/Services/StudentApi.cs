namespace Student_Affair_DDD.Web.Client.Services;

public sealed class StudentApi : IStudentApi
{
    private const string BaseUrl = "api/students";
    private readonly HttpClient _http;
    public StudentApi(HttpClient http) => _http = http;

    public async Task<PagedResult<StudentDto>> GetPagedAsync(StudentQuery query, CancellationToken cancellationToken = default)
    {
        return await _http.GetFromJsonAsync<PagedResult<StudentDto>>($"{BaseUrl}?{query.ToQueryString()}", cancellationToken)
            ?? throw new InvalidOperationException("The API returned an empty students response.");
    }

    public async Task<IReadOnlyList<StudentDto>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _http.GetFromJsonAsync<IReadOnlyList<StudentDto>>($"{BaseUrl}/all", cancellationToken) ?? [];

    public async Task<StudentDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        using var response = await _http.GetAsync($"{BaseUrl}/{id}", cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<StudentDto>(cancellationToken: cancellationToken);
    }

    public async Task<StudentDto> CreateAsync(StudentCreateDto dto, CancellationToken cancellationToken = default)
    {
        using var response = await _http.PostAsJsonAsync(BaseUrl, dto, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<StudentDto>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("The API returned an empty student response.");
    }

    public async Task<StudentDto?> UpdateAsync(int id, StudentUpdateDto dto, CancellationToken cancellationToken = default)
    {
        using var response = await _http.PutAsJsonAsync($"{BaseUrl}/{id}", dto, cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<StudentDto>(cancellationToken: cancellationToken);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        using var response = await _http.DeleteAsync($"{BaseUrl}/{id}", cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound)
            return false;

        response.EnsureSuccessStatusCode();
        return true;
    }

    public async Task<IReadOnlyList<string>> GetDepartmentsAsync(CancellationToken cancellationToken = default)
        => await _http.GetFromJsonAsync<IReadOnlyList<string>>($"{BaseUrl}/departments", cancellationToken) ?? [];
}
