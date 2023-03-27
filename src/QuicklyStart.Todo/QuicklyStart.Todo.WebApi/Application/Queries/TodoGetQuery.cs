namespace QuicklyStart.Todo.WebApi.Application.Queries;

public record TodoGetQuery(Guid Id) : Query<TodoGetDto>
{
    public override TodoGetDto Result { get; set; }
}
