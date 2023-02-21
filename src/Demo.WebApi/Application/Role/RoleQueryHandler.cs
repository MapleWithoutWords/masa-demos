using Demo.WebApi.Application.BaseDto;
using Demo.WebApi.Application.Role.Dtos;
using Demo.WebApi.Application.Role.Queries;
using Demo.WebApi.Domain.AuthManager;
using Demo.WebApi.Domain.AuthManager.Entities;
using Demo.WebApi.Infrastructures;
using Masa.BuildingBlocks.Ddd.Domain.Repositories;
using Masa.Contrib.Dispatcher.Events;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Demo.WebApi.Application.Role
{
    public class RoleQueryHandler
    {
        private readonly IRoleRepository _roleRepository;

        public RoleQueryHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [EventHandler]
        public async Task QueryListHanlderAsync(RoleGetListQuery query)
        {
            Expression<Func<RoleEntity, bool>> expression = e => true;
            if (query.Keyword.IsNullOrEmpty() == false)
            {
                expression = e => e.Name.Contains(query.Keyword) || e.Remark.Contains(query.Keyword);
            }

            var queryable = _roleRepository.GetQueryable(expression);

            var roleCount = await queryable.CountAsync();

            var roles = await queryable.Skip(((query.PageIndex - 1) * query.PageSize)).Take(query.PageSize).ToListAsync();


            query.Result = new PaginatedItemsViewModel<RoleGetListOutput>(query.PageIndex, query.PageSize, roleCount, roles.Select(e => new RoleGetListOutput
            {
                CreationTime = e.CreationTime,
                Id = e.Id,
                Name = e.Name,
                Remark = e.Remark,
            }
            ).ToList());

        }
    }
}
