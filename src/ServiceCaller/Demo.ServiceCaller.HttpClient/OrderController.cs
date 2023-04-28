using Microsoft.AspNetCore.Mvc;

namespace Demo.ServiceCaller.HttpClient
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OrderController : ControllerBase
    {
        private readonly UserCaller _userCaller;

        public OrderController(UserCaller userCaller) => _userCaller = userCaller;

        public async Task<IActionResult> CreateAsync(int userId)
        {
            var userObj = await _userCaller.GetAsync(userId);
            return Ok(userObj);
        }
    }
}
