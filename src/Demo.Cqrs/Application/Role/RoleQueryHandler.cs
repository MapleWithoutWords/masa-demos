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
using Masa.BuildingBlocks.Caching;

namespace Demo.WebApi.Application.Role
{
    public class RoleQueryHandler
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMultilevelCacheClient _cacheClient;

        public RoleQueryHandler(IRoleRepository roleRepository, IMultilevelCacheClient cacheClient)
        {
            _roleRepository = roleRepository;
            _cacheClient = cacheClient;
        }

        private const string RoleQueryCacheKey = "RoleQueryKey";

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

            var cacheResultData = await _cacheClient.GetAsync<PaginatedItemsViewModel<RoleGetListOutput>>(RoleQueryCacheKey);
            if (cacheResultData != null)
            {
                query.Result = cacheResultData;
                return;
            }
            else
            {
                query.Result = new PaginatedItemsViewModel<RoleGetListOutput>(query.PageIndex, query.PageSize, roleCount, roles.Select(e => new RoleGetListOutput
                {
                    CreationTime = e.CreationTime,
                    Id = e.Id,
                    Name = e.Name,
                    Remark = e.Remark,
                }
                ).ToList());
                await _cacheClient.SetAsync<PaginatedItemsViewModel<RoleGetListOutput>>(RoleQueryCacheKey,query.Result);
            }


        }
    }
}
