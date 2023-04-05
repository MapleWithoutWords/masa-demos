using Demo.EventBus.Application.EventData;
using Masa.Contrib.Dispatcher.Events;

namespace Demo.EventBus.Application;

public class UserService
{
    [EventHandler(1)]
    public async Task UpdateUserAsync(UpdateUserEvent @event)
    {
        Console.WriteLine($"=================【{nameof(UserService.UpdateUserAsync)}】update user logic=================");
    }
}
