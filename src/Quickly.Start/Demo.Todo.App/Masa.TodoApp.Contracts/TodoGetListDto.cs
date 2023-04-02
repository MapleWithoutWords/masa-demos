namespace Masa.TodoApp.Contracts;

public class TodoGetListDto
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public bool Done { get; set; }
}
