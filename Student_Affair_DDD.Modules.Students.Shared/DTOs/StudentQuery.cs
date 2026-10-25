using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Affair_DDD.Modules.Students.Shared.DTOs
{
    public class StudentQuery
    {
        public string? Search { get; set; }
        public string? Department { get; set; }
        public bool? IsActive { get; set; }

        /// <summary>One of: name, studentNumber, department, gpa, enrollmentDate. Defaults to name.</summary>
        public string? SortBy { get; set; }
        public bool SortDescending { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string ToQueryString()
        {
            var parts = new List<string>
        {
            $"page={Page}",
            $"pageSize={PageSize}",
            $"sortDescending={SortDescending.ToString().ToLowerInvariant()}"
        };
            if (!string.IsNullOrWhiteSpace(Search)) parts.Add($"search={Uri.EscapeDataString(Search)}");
            if (!string.IsNullOrWhiteSpace(Department)) parts.Add($"department={Uri.EscapeDataString(Department)}");
            if (IsActive is not null) parts.Add($"isActive={IsActive.Value.ToString().ToLowerInvariant()}");
            if (!string.IsNullOrWhiteSpace(SortBy)) parts.Add($"sortBy={Uri.EscapeDataString(SortBy)}");
            return string.Join("&", parts);
        }
    }
}
