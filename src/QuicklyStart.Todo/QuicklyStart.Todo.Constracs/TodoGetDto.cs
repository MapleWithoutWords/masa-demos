namespace QuicklyStart.Todo.Constracs;

public class TodoGetDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public bool Done { get; set; }
}
