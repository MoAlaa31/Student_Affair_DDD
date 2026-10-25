using Student_Affair_DDD.Modules.Teachers.Shared.DTOs;

namespace Student_Affair_DDD.Web.Client.Pages
{
    public partial class Teachers
    {
        private readonly TeacherQuery query = new();
        private TeacherCreateDto createModel = new();
        private PagedTeachers? page;
        private string? error;
        private int? editingId;

        protected override async Task OnInitializedAsync() => await LoadAsync();

        private async Task LoadAsync()
        {
            try
            {
                page = await TeacherApi.GetPagedAsync(query);
                error = null;
            }
            catch (Exception exception)
            {
                error = exception.Message;
            }
        }

        private async Task CreateAsync()
        {
            try
            {
                if (editingId is int id)
                {
                    var updated = await TeacherApi.UpdateAsync(id, new TeacherUpdateDto
                    {
                        EmployeeNumber = createModel.EmployeeNumber,
                        FirstName = createModel.FirstName,
                        LastName = createModel.LastName,
                        Email = createModel.Email,
                        Department = createModel.Department
                    });
                    if (updated is null)
                    {
                        await LoadAsync();
                        error = "This teacher no longer exists.";
                        return;
                    }
                }
                else
                {
                    await TeacherApi.CreateAsync(createModel);
                }

                editingId = null;
                createModel = new TeacherCreateDto();
                await LoadAsync();
            }
            catch (Exception exception)
            {
                error = exception.Message;
            }
        }

        private void EditTeacher(TeacherDto teacher)
        {
            editingId = teacher.Id;
            createModel = new TeacherCreateDto
            {
                EmployeeNumber = teacher.EmployeeNumber,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Email = teacher.Email,
                Department = teacher.Department
            };
            error = null;
        }

        private void CancelEdit()
        {
            editingId = null;
            createModel = new TeacherCreateDto();
            error = null;
        }
    }
}
