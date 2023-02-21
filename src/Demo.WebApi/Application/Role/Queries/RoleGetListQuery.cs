using Demo.WebApi.Application.BaseDto;
using Demo.WebApi.Application.Role.Dtos;
using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Queries;

namespace Demo.WebApi.Application.Role.Queries
{
    public record RoleGetListQuery : Query<PaginatedItemsViewModel<RoleGetListOutput>>
    {
        public int PageSize { get; set; } = default!;
        public int PageIndex { get; set; } = default!;
        public string Keyword { get; set; } = default!;

        public override PaginatedItemsViewModel<RoleGetListOutput> Result { set; get; } = default!;

    }
}
