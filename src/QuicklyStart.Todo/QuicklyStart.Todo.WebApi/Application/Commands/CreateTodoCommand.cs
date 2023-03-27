namespace QuicklyStart.Todo.WebApi.Application.Commands;

public record CreateTodoCommand(TodoCreateUpdateDto Dto) : Command { }
