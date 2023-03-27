namespace QuicklyStart.Todo.WebApi.Services;

public class TodoService : ServiceBase
{
    public async Task<PaginatedListBase<TodoGetListDto>> GetListAsync(IEventBus eventBus, string? keyword, bool? done, int pageIndex = -1, int pageDataCount = 10)
    {
        var todoQuery = new TodoGetListQuery(keyword, done, pageIndex, pageDataCount);
        await eventBus.PublishAsync(todoQuery);
        return todoQuery.Result;
    }

    public async Task<TodoGetDto> GetAsync(IEventBus eventBus, Guid id)
    {
        var todoQuery = new TodoGetQuery(id);
        await eventBus.PublishAsync(todoQuery);
        return todoQuery.Result;
    }

    public async Task CreateAsync(IEventBus eventBus, TodoCreateUpdateDto dto)
    {
        var command = new CreateTodoCommand(dto);
        await eventBus.PublishAsync(command);
    }

    public async Task UpdateAsync(IEventBus eventBus, Guid id, TodoCreateUpdateDto dto)
    {
        var command = new UpdateTodoCommand(id, dto);
        await eventBus.PublishAsync(command);
    }

    public async Task DeleteAsync(IEventBus eventBus, Guid id)
    {
        var command = new DeleteTodoCommand(id);
        await eventBus.PublishAsync(command);
    }

    [HttpPost("done")]
    public async Task DoneAsync(IEventBus eventBus, Guid id, bool done)
    {
        var command = new DoneTodoCommand(id, done);
        await eventBus.PublishAsync(command);
    }
}