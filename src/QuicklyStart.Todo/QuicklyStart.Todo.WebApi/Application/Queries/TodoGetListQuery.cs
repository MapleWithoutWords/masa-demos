
namespace QuicklyStart.Todo.WebApi.Application.Queries
{
    public record TodoGetListQuery(string Keyword, bool? Done) : Query<PaginatedListBase<TodoGetDto>>
    {
        public override PaginatedListBase<TodoGetDto> Result { get; set; }
    }
}
