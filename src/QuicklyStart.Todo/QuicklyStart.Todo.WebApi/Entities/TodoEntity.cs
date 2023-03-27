namespace QuicklyStart.Todo.WebApi.Entities;
public class TodoEntity : Entity<Guid>
{
    public string Title { get; set; }
    public bool Done { get; set; }
}
