namespace QuicklyStart.Todo.WebApi.Application.Queries;

public record TodoGetListQuery(string? Keyword, bool? Done, int? PageIndex = 1, int PageDataCount = 10) : Query<PaginatedListBase<TodoGetListDto>>
{
    public override PaginatedListBase<TodoGetListDto> Result { get; set; } = new();
}
