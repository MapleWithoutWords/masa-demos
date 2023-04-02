using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Queries;
using Masa.TodoApp.Contracts;

namespace Masa.TodoApp.WebApi.Application.Queries;

public record TodoGetListQuery : Query<List<TodoGetListDto>>
{
    public override List<TodoGetListDto> Result { get; set; }
}
