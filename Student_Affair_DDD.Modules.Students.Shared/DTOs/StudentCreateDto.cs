using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Student_Affair_DDD.Modules.Students.Shared.DTOs
{
    public class StudentCreateDto
    {
        [Required, StringLength(100, MinimumLength = 2)]
        public string FirstName { get; set; } = string.Empty;

        [Required, StringLength(100, MinimumLength = 2)]
        public string LastName { get; set; } = string.Empty;

        [Required, StringLength(20, MinimumLength = 3)]
        [RegularExpression(@"^[A-Za-z0-9\-]+$", ErrorMessage = "Only letters, digits and dashes are allowed.")]
        public string StudentNumber { get; set; } = string.Empty;

        [Required, StringLength(20, MinimumLength = 5)]
        public string NationalId { get; set; } = string.Empty;

        [Required, EmailAddress, StringLength(200)]
        public string Email { get; set; } = string.Empty;

        [Phone, StringLength(20)]
        public string? PhoneNumber { get; set; }

        [Required]
        public DateOnly DateOfBirth { get; set; } = new DateOnly(2005, 1, 1);

        [Required, EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; } = Gender.Unspecified;

        [Required, EnumDataType(typeof(AcademicLevel))]
        public AcademicLevel Level { get; set; } = AcademicLevel.Freshman;

        [Required, StringLength(100, MinimumLength = 2)]
        public string Department { get; set; } = string.Empty;

        [Range(0.0, 4.0, ErrorMessage = "GPA must be between 0.00 and 4.00.")]
        public decimal Gpa { get; set; }

        [Required]
        public DateOnly EnrollmentDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

        public bool IsActive { get; set; } = true;
    }
}
