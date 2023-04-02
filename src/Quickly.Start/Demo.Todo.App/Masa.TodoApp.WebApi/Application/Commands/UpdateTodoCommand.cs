using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Commands;
using Masa.TodoApp.Contracts;

namespace Masa.TodoApp.WebApi.Application.Commands;

public record UpdateTodoCommand(Guid Id, TodoCreateUpdateDto Dto) : Command { }
