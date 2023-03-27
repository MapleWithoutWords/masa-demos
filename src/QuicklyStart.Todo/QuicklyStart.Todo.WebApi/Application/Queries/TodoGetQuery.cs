namespace QuicklyStart.Todo.WebApi.Application.Queries
{
    public record TodoGetQuery : Query<TodoGetDto>
    {
        public override TodoGetDto Result { get; set; }
    }
}
