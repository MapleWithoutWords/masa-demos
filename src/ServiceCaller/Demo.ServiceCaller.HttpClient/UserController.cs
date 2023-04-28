using Masa.BuildingBlocks.Service.Caller;
using Microsoft.AspNetCore.Mvc;

namespace Demo.ServiceCaller.HttpClient;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly ICallerFactory _callerFactory;

    public UserController(ICallerFactory callerFactory) => _callerFactory = callerFactory;

    public async Task<IActionResult> CreateAsync(int userId)
    {
        using var caller = _callerFactory.Create("ServiceName");
        var userObj = await caller.GetAsync($"getlist?userId={userId}");
        return Ok(userObj);
    }
}
