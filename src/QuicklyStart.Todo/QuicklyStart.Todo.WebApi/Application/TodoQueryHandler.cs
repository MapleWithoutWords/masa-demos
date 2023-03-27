
namespace QuicklyStart.Todo.WebApi.Application;
public class TodoQueryHandler
{
    readonly TodoDbContext _todoDbContext;

    public TodoQueryHandler(TodoDbContext todoDbContext)
    {
        _todoDbContext = todoDbContext;
    }

    public async Task GetListAsync(TodoGetListQuery query)
    {
        var todoDbQuery = _todoDbContext.Set<TodoEntity>().AsNoTracking();
        if (query.Keyword.IsNullOrEmpty())
        {
            todoDbQuery = todoDbQuery.Where(e => e.Title.Contains(query.Keyword));
        }
        if (query.Done is not null)
        {
            todoDbQuery = todoDbQuery.Where(e => e.Done == query.Done.Value);
        }

    }
}
