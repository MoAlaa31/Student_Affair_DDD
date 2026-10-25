using Microsoft.AspNetCore.Components;

namespace BlazorAppServer;

public partial class Students
{
    [Inject]
    public IStudentApi StudentApi { get; set; } = default!;

    public StudentFormModel student = new();
    public List<StudentDto> students = [];
    public Student_Affair_DDD.Web.Client.Modal? modalComponent;

    private StudentDto? pendingDeleteStudent;
    private string? errorMessage;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            students = (await StudentApi.GetAllAsync()).ToList();
        }
        catch (Exception exception)
        {
            errorMessage = exception.Message;
        }
    }

    private async Task HandleValidSubmit()
    {
        errorMessage = null;

        try
        {
            if (student.Id == 0)
                await StudentApi.CreateAsync(student.ToCreateDto());
            else if (await StudentApi.UpdateAsync(student.Id, student.ToUpdateDto()) is null)
            {
                errorMessage = "The student no longer exists.";
                return;
            }

            students = (await StudentApi.GetAllAsync()).ToList();
            student = new StudentFormModel();
        }
        catch (Exception exception)
        {
            errorMessage = exception.Message;
        }
    }

    private void EditStudent(StudentDto item) => student = StudentFormModel.FromDto(item);

    private async Task ShowModal(StudentDto item)
    {
        pendingDeleteStudent = item;
        if (modalComponent is not null)
            await modalComponent.ShowModal();
    }

    private async Task DeleteStudent()
    {
        if (pendingDeleteStudent is null)
            return;

        var target = pendingDeleteStudent;
        pendingDeleteStudent = null;

        try
        {
            if (!await StudentApi.DeleteAsync(target.Id))
                errorMessage = "The student no longer exists.";

            students = (await StudentApi.GetAllAsync()).ToList();
            if (student.Id == target.Id)
                student = new StudentFormModel();
        }
        catch (Exception exception)
        {
            errorMessage = exception.Message;
        }
    }

    private void Clear()
    {
        student = new StudentFormModel();
        errorMessage = null;
    }
}
