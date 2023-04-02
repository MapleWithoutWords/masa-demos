using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Commands;

namespace Masa.TodoApp.WebApi.Application.Commands; 

public record DoneTodoCommand(Guid Id, bool Done) : Command { }
