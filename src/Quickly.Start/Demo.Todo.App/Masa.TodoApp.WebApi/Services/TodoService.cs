using Masa.BuildingBlocks.Dispatcher.Events;
using Masa.TodoApp.Contracts;
using Masa.TodoApp.WebApi.Application.Commands;
using Masa.TodoApp.WebApi.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Masa.TodoApp.WebApi.Services;

public class TodoService : ServiceBase
{
    private IEventBus _eventBus => GetRequiredService<IEventBus>();

    public async Task<List<TodoGetListDto>> GetListAsync()
    {
        var todoQuery = new TodoGetListQuery();
        await _eventBus.PublishAsync(todoQuery);
        return todoQuery.Result;
    }

    public async Task CreateAsync(TodoCreateUpdateDto dto)
    {
        var command = new CreateTodoCommand(dto);
        await _eventBus.PublishAsync(command);
    }

    public async Task UpdateAsync(Guid id, TodoCreateUpdateDto dto)
    {
        var command = new UpdateTodoCommand(id, dto);
        await _eventBus.PublishAsync(command);
    }

    public async Task DeleteAsync(Guid id)
    {
        var command = new DeleteTodoCommand(id);
        await _eventBus.PublishAsync(command);
    }

    public async Task DoneAsync([FromQuery] Guid id, [FromQuery] bool done)
    {
        var command = new DoneTodoCommand(id, done);
        await _eventBus.PublishAsync(command);
    }
}
