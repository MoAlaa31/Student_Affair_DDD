namespace Student_Affair_DDD.Web.Client.Pages
{
    public partial class Classrooms
    {
        private readonly ClassroomQuery query = new();
        private ClassroomCreateDto createModel = new();
        private PagedClassrooms? page;
        private string? error;
        private int? editingId;

        protected override async Task OnInitializedAsync() => await LoadAsync();

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
                        Capacity = createModel.Capacity
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
                Capacity = classroom.Capacity
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
