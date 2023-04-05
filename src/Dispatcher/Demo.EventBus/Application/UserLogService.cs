using Demo.EventBus.Application.EventData;
using Masa.Contrib.Dispatcher.Events;

namespace Demo.EventBus.Application;

public class UserLogService
{
    [EventHandler(3)]
    public async Task UpdateUserLogAsync(UpdateUserEvent @event)
    {
        Console.WriteLine($"=================【{nameof(UserLogService.UpdateUserLogAsync)}】update user log=================");
    }
}
