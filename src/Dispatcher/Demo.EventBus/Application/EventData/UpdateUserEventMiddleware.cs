using Masa.BuildingBlocks.Dispatcher.Events;

namespace Demo.EventBus.Application.EventData;

public class UpdateUserEventMiddleware : IEventMiddleware<UpdateUserEvent>
{
    public bool SupportRecursive => true;

    public async Task HandleAsync(UpdateUserEvent @event, EventHandlerDelegate next)
    {
        if (@event.Dto.UserName.IsNullOrEmpty())
        {
            Console.WriteLine($"UserName cann't is null or empty");
            return;
        }
        await next();
    }
}
