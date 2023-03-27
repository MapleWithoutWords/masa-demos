namespace QuicklyStart.Todo.WebApi.Application.Commands
{
    public record DoneTodoCommand(Guid Id, bool Done) : Command
    {
    }
}
