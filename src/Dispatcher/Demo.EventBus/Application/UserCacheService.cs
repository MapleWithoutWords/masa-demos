using Demo.EventBus.Application.EventData;
using Masa.Contrib.Dispatcher.Events;

namespace Demo.EventBus.Application;

public class UserCacheService
{
    [EventHandler(2)]
    public async Task UpdateUserCacheAsync(UpdateUserEvent @event)
    {
        Console.WriteLine($"=================【{nameof(UserCacheService.UpdateUserCacheAsync)}】update user cache=================");
    }
}
