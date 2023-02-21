using Demo.WebApi.Domain.AuthManager.Entities;
using Masa.BuildingBlocks.Data;
using Microsoft.EntityFrameworkCore;

namespace Demo.WebApi.Infrastructures
{
    public class DemoDbContext : MasaDbContext<DemoDbContext>
    {
        public virtual DbSet<RoleEntity> Roles { get; set; }
        public virtual DbSet<FeatureEntity> Features { get; set; }

        public DemoDbContext(MasaDbContextOptions<DemoDbContext> options) : base(options)
        {
        }
    }
}
