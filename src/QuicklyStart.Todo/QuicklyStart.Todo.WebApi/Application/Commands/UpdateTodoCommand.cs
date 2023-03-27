namespace QuicklyStart.Todo.WebApi.Application.Commands; 

public record UpdateTodoCommand(Guid Id, TodoCreateUpdateDto Dto) : Command { }
