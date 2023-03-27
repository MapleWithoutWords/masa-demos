namespace QuicklyStart.Todo.WebApi.Application;

public class TodoQueryHandler
{
    readonly TodoDbContext _todoDbContext;

    public TodoQueryHandler(TodoDbContext todoDbContext) => _todoDbContext = todoDbContext;

    [EventHandler]
    public async Task GetListAsync(TodoGetListQuery query)
    {
        var todoDbQuery = _todoDbContext.Set<TodoEntity>().AsNoTracking();
        if (query.Keyword.IsNullOrEmpty() == false)
        {
            todoDbQuery = todoDbQuery.Where(e => e.Title.Contains(query.Keyword));
        }
        if (query.Done is not null)
        {
            todoDbQuery = todoDbQuery.Where(e => e.Done == query.Done.Value);
        }

        if (query.PageIndex != null && query.PageIndex > 0)
        {
            todoDbQuery = todoDbQuery.Take((query.PageIndex.Value - 1) * query.PageDataCount).Skip(query.PageDataCount);
        }
        query.Result.Result = await todoDbQuery.Select(e => e.Adapt<TodoGetListDto>()).ToListAsync();
        query.Result.Total = await todoDbQuery.LongCountAsync();
    }

    [EventHandler]
    public async Task GetAsync(TodoGetQuery query)
    {
        var todoDbData = await _todoDbContext.Set<TodoEntity>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == query.Id);
        query.Result = todoDbData.Adapt<TodoGetDto>();
    }
}
