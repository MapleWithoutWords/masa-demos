using Demo.WebApi.Application.BaseDto;
using Demo.WebApi.Application.Role.Dtos;
using Demo.WebApi.Application.Role.Queries;
using Masa.BuildingBlocks.Dispatcher.Events;
using Microsoft.AspNetCore.Mvc;

namespace Demo.WebApi.Services
{
    public class RoleService : ServiceBase
    {
        public RoleService() : base("api/role")
        {

        }

        [ProducesDefaultResponseType(typeof(PaginatedItemsViewModel<RoleGetListOutput>))]
        public async Task<IResult> GetListAsync([FromServices] IEventBus eventBus,
            [FromQuery] string keyword,
            [FromQuery] int pageSize = 10,
            [FromQuery] int pageIndex = 1)
        {

            var query = new RoleGetListQuery()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            await eventBus.PublishAsync(query);
            return Results.Ok(query.Result);

        }

        public async Task<IResult> GetAsync(Guid id)
        {
            return Results.Ok();
        }

        public async Task<IResult> CreateAsync()
        {
            return Results.Accepted();
        }

        public async Task<IResult> UpdateAsync(Guid id)
        {
            return Results.Accepted();
        }

        public async Task<IResult> DeleteAsync(Guid id)
        {
            return Results.Accepted();
        }
    }
}
