using Demo.EventBus.Application.Dtos;
using Masa.BuildingBlocks.Dispatcher.Events;

namespace Demo.EventBus.Application.EventData;

public record UpdateUserEvent(UserUpdateDto Dto) : Event { }
