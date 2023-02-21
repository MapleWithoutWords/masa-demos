using Demo.WebApi.Domain.AuthManager.Entities;
using System.Linq.Expressions;

namespace Demo.WebApi.Domain.AuthManager
{
    public interface IRoleRepository
    {

        public IQueryable<RoleEntity> GetQueryable(Expression<Func<RoleEntity,bool>> expression=null);
    }
}
