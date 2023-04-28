using Microsoft.AspNetCore.Mvc;

namespace Demo.ServiceCaller.DaprClient;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly UserCaller _userCaller;

    public UserController(UserCaller userCaller) => _userCaller = userCaller;

    public async Task<IActionResult> CreateAsync(int userId)
    {
        var userObj = await _userCaller.HelloAsync("word");
        return Ok(userObj);
    }
}
