using Student_Affair_DDD.Modules.Students.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Student_Affair_DDD.Modules.Students.Shared.DTOs
{
    public class StudentUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required, StringLength(100, MinimumLength = 2)]
        public string FirstName { get; set; } = string.Empty;

        [Required, StringLength(100, MinimumLength = 2)]
        public string LastName { get; set; } = string.Empty;

        [Required, EmailAddress, StringLength(200)]
        public string Email { get; set; } = string.Empty;

        [Phone, StringLength(20)]
        public string? PhoneNumber { get; set; }

        [Required]
        public DateOnly DateOfBirth { get; set; }

        [Required, EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }

        [Required, EnumDataType(typeof(AcademicLevel))]
        public AcademicLevel Level { get; set; }

        [Required, StringLength(100, MinimumLength = 2)]
        public string Department { get; set; } = string.Empty;

        [Range(0.0, 4.0, ErrorMessage = "GPA must be between 0.00 and 4.00.")]
        public decimal Gpa { get; set; }

        [Required]
        public DateOnly EnrollmentDate { get; set; }

        public bool IsActive { get; set; }
    }
}
