using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Commands;

namespace Masa.TodoApp.WebApi.Application.Commands;

public record DeleteTodoCommand(Guid Id) : Command { }
