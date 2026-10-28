namespace Student_Affair_DDD.Web.Client.Pages
{
    public partial class Classrooms
    {
        private readonly ClassroomQuery query = new();
        private ClassroomCreateDto createModel = new();
        private PagedClassrooms? page;
        private IReadOnlyList<StudentDto> availableStudents = [];
        private IReadOnlyList<TeacherDto> availableTeachers = [];
        private string? error;
        private string? peopleError;
        private int? editingId;

        protected override async Task OnInitializedAsync()
        {
            await LoadPeopleAsync();
            await LoadAsync();
        }

        private async Task LoadPeopleAsync()
        {
            try
            {
                var studentTask = StudentApi.GetAllAsync();
                var teacherTask = TeacherApi.GetPagedAsync(new TeacherQuery { PageSize = 100, IsActive = true });
                await Task.WhenAll(studentTask, teacherTask);
                availableStudents = (await studentTask).Where(student => student.IsActive).ToArray();
                availableTeachers = (await teacherTask).Items.Where(teacher => teacher.IsActive).ToArray();
            }
            catch (Exception exception)
            {
                peopleError = $"Could not load active students and teachers: {exception.Message}";
            }
        }

        private async Task LoadAsync()
        {
            try
            {
                page = await ClassroomApi.GetPagedAsync(query);
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
                    var updated = await ClassroomApi.UpdateAsync(id, new ClassroomUpdateDto
                    {
                        Code = createModel.Code,
                        Building = createModel.Building,
                        RoomNumber = createModel.RoomNumber,
                        Capacity = createModel.Capacity,
                        StudentIds = createModel.StudentIds,
                        TeacherIds = createModel.TeacherIds
                    });
                    if (updated is null)
                    {
                        await LoadAsync();
                        error = "This classroom no longer exists.";
                        return;
                    }
                }
                else
                {
                    await ClassroomApi.CreateAsync(createModel);
                }

                editingId = null;
                createModel = new ClassroomCreateDto();
                await LoadAsync();
            }
            catch (Exception exception)
            {
                error = exception.Message;
            }
        }

        private void EditClassroom(ClassroomDto classroom)
        {
            editingId = classroom.Id;
            createModel = new ClassroomCreateDto
            {
                Code = classroom.Code,
                Building = classroom.Building,
                RoomNumber = classroom.RoomNumber,
                Capacity = classroom.Capacity,
                StudentIds = classroom.StudentIds.ToArray(),
                TeacherIds = classroom.TeacherIds.ToArray()
            };
            error = null;
        }

        private void CancelEdit()
        {
            editingId = null;
            createModel = new ClassroomCreateDto();
            error = null;
        }
    }
}
