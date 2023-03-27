using Microsoft.EntityFrameworkCore;

namespace QuicklyStart.Todo.WebApi.Entities
{
    public class TodoDbContext : MasaDbContext<TodoDbContext>
    {
        public DbSet<TodoEntity> Todos { get; set; }

        public TodoDbContext(MasaDbContextOptions<TodoDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreatingConfigureGlobalFilters(ModelBuilder modelBuilder)
        {
            base.OnModelCreatingConfigureGlobalFilters(modelBuilder);

            ConfigEntities(modelBuilder);
        }

        private static void ConfigEntities(ModelBuilder modelBuilder)
        {
            var todoBuilder = modelBuilder.Entity<TodoEntity>();
            todoBuilder.Property(e => e.Title).HasMaxLength(128);
        }
    }
}
