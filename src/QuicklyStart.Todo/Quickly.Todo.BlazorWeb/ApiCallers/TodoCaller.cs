namespace Quickly.Todo.BlazorWeb.ApiCallers;

public class TodoCaller : HttpClientCallerBase
{
    private readonly string _prefix = "/api/v1/todoes";
    protected override string BaseAddress { get; set; }

    public TodoCaller(IOptions<TodoCallerOptions> options)
    {
        BaseAddress = options.Value.BaseAddress;
    }

    public async Task<PaginatedListBase<TodoGetListDto>> GetListAsync()
    {
        var result = await Caller.GetAsync<PaginatedListBase<TodoGetListDto>>($"{_prefix}/list");

        return result ?? new();
    }
    public async Task<TodoGetDto> GetAsync(Guid id)
    {
        var result = await Caller.GetAsync<TodoGetDto>($"{_prefix}/{id}");
        return result ?? new();
    }

    public async Task CreateAsync(TodoCreateUpdateDto dto)
    {
        var result = await Caller.PostAsync($"{_prefix}", dto);
    }

    public async Task UpdateAsync(Guid id, TodoCreateUpdateDto dto)
    {
        var result = await Caller.PutAsync($"{_prefix}/{id}", dto);
    }

    public async Task DeleteAsync(Guid id)
    {
        var result = await Caller.DeleteAsync($"{_prefix}/{id}", null);
    }

    public async Task DoneAsync(Guid id, bool done)
    {
        var result = await Caller.DeleteAsync($"{_prefix}/done?id={id}&done={done.ToString().ToLower()}", null);
    }
}
