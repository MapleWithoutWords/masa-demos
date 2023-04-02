using Masa.Contrib.Service.Caller.HttpClient;
using Masa.TodoApp.Contracts;
using Microsoft.Extensions.Options;

namespace Masa.TodoApp.WebBalzor.ApiCallers;

public class TodoCaller : HttpClientCallerBase
{
    protected override string BaseAddress { get; set; }

    public TodoCaller(IOptions<TodoServiceOptions> options)
    {
        BaseAddress = options.Value.BaseAddress;
        Prefix = "/api/v1/todoes";
    }

    public async Task<List<TodoGetListDto>> GetListAsync()
    {
        var result = await Caller.GetAsync<List<TodoGetListDto>>($"list");
        return result ?? new();
    }

    public async Task CreateAsync(TodoCreateUpdateDto dto)
    {
        var result = await Caller.PostAsync($"", dto);
    }

    public async Task UpdateAsync(Guid id, TodoCreateUpdateDto dto)
    {
        var result = await Caller.PutAsync($"{id}", dto);
    }

    public async Task DeleteAsync(Guid id)
    {
        var result = await Caller.DeleteAsync($"{id}", null);
    }

    public async Task DoneAsync(Guid id, bool done)
    {
        var result = await Caller.PostAsync($"done?id={id}&done={done}", null);
    }
}
