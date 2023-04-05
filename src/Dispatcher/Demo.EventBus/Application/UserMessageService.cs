using Demo.EventBus.Application.EventData;
using Masa.Contrib.Dispatcher.Events;

namespace Demo.EventBus.Application;

public class UserMessageService
{
    [EventHandler(3)]
    public async Task UpdateUserMessageAsync(UpdateUserEvent @event)
    {
        Console.WriteLine($"=================【{nameof(UserMessageService.UpdateUserMessageAsync)}】update user message=================");
    }
}
