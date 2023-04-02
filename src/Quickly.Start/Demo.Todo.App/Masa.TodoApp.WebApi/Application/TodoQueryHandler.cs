using Mapster;
using Masa.Contrib.Dispatcher.Events;
using Masa.TodoApp.Contracts;
using Masa.TodoApp.WebApi.Application.Queries;
using Masa.TodoApp.WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace Masa.TodoApp.WebApi.Application;

public class TodoQueryHandler
{
    readonly TodoDbContext _todoDbContext;

    public TodoQueryHandler(TodoDbContext todoDbContext) => _todoDbContext = todoDbContext;

    [EventHandler]
    public async Task GetListAsync(TodoGetListQuery query)
    {
        var todoDbQuery = _todoDbContext.Set<TodoEntity>();
        query.Result = await todoDbQuery.Select(e => e.Adapt<TodoGetListDto>()).ToListAsync();
    }
}
