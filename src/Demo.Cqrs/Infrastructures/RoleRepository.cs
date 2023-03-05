using Demo.WebApi.Domain.AuthManager;
using Demo.WebApi.Domain.AuthManager.Entities;
using System.Linq.Expressions;

namespace Demo.WebApi.Infrastructures
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DemoDbContext _context;

        public RoleRepository(DemoDbContext context) => _context = context;
        public IQueryable<RoleEntity> GetQueryable(Expression<Func<RoleEntity, bool>> expression = null)
        {
            var query = _context.Roles.AsQueryable();
            if (expression != null)
            {
                query = query.Where(expression);
            }
            return query;
        }
    }
}
