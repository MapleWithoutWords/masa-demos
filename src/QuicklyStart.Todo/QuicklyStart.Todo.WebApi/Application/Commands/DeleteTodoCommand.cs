namespace QuicklyStart.Todo.WebApi.Application.Commands;

public record DeleteTodoCommand(Guid Id) : Command { }
