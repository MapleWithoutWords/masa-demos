using Mapster;
using Masa.Contrib.Dispatcher.Events;
using Masa.TodoApp.WebApi.Application.Commands;
using Masa.TodoApp.WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace Masa.TodoApp.WebApi.Application;

public class TodoCommandHandler
{
    readonly TodoDbContext _todoDbContext;

    public TodoCommandHandler(TodoDbContext todoDbContext) => _todoDbContext = todoDbContext;

    [EventHandler]
    public async Task CreateAsync(CreateTodoCommand command)
    {
        await ValidateAsync(command.Dto.Title);
        var todo = command.Dto.Adapt<TodoEntity>();
        await _todoDbContext.Set<TodoEntity>().AddAsync(todo);
        await _todoDbContext.SaveChangesAsync();
    }

    [EventHandler]
    public async Task UpdateAsync(UpdateTodoCommand command)
    {
        await ValidateAsync(command.Dto.Title, command.Id);
        var todo = await _todoDbContext.Set<TodoEntity>().AsNoTracking().FirstOrDefaultAsync(t => t.Id == command.Id);
        if (todo == null)
        {
            throw new UserFriendlyException("代办不存在");
        }
        command.Dto.Adapt(todo);
        _todoDbContext.Set<TodoEntity>().Update(todo);
        await _todoDbContext.SaveChangesAsync();
    }

    private async Task ValidateAsync(string title, Guid? id = null)
    {
        var todo = await _todoDbContext.Set<TodoEntity>().AsNoTracking().FirstOrDefaultAsync(t => t.Title == title && t.Id != id);
        if (todo != null)
            throw new UserFriendlyException("代办已存在");
    }

    [EventHandler]
    public async Task DeleteAsync(DeleteTodoCommand command)
    {
        var todo = await _todoDbContext.Set<TodoEntity>().AsNoTracking().FirstOrDefaultAsync(t => t.Id == command.Id);
        if (todo == null)
        {
            return;
        }
        _todoDbContext.Set<TodoEntity>().Remove(todo);
        await _todoDbContext.SaveChangesAsync();
    }

    [EventHandler]
    public async Task DoneAsync(DoneTodoCommand command)
    {
        var todo = await _todoDbContext.Set<TodoEntity>().AsNoTracking().FirstOrDefaultAsync(t => t.Id == command.Id);
        if (todo == null)
        {
            return;
        }
        todo.Done = command.Done;
        _todoDbContext.Set<TodoEntity>().Update(todo);
        await _todoDbContext.SaveChangesAsync();
    }
}
