using Demo.EventBus.Application.Dtos;
using Demo.EventBus.Application.EventData;
using Masa.BuildingBlocks.Dispatcher.Events;
using Microsoft.AspNetCore.Mvc;

namespace Demo.EventBus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IEventBus _eventBus;

        public UserController(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAsync([FromQuery]UserUpdateDto dto)
        {
            await _eventBus.PublishAsync(new UpdateUserEvent(dto));
            return Ok();
        }
    }
}
