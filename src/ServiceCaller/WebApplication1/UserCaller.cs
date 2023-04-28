using Masa.Contrib.Service.Caller.HttpClient;
using Microsoft.Extensions.Options;

namespace WebApplication1;

public class UserCaller : HttpClientCallerBase
{
    protected override string Prefix { get; set; } = "api/users";

    protected override string BaseAddress { get; set; }

    public UserCaller(IOptions<ServiceCallerOptions> options)
    {
        this.BaseAddress = options.Value.UserServiceBaseAddress;
    }

    public async Task<object?> GetAsync(int id)
    {
        return await Caller.GetAsync<object>($"{id}");
    }
}
