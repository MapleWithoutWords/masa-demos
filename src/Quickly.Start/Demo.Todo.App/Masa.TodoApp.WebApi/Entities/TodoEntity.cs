using Masa.BuildingBlocks.Ddd.Domain.Entities;

namespace Masa.TodoApp.WebApi.Entities;

public class TodoEntity : Entity<Guid>
{
    public string Title { get; set; }

    public bool Done { get; set; }
}
