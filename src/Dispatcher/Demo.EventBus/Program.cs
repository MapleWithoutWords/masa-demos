using Demo.EventBus.Application.EventData;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEventBus(opt=>opt.UseMiddleware(typeof(UpdateUserEventMiddleware)));

var app = builder.Build();

app.MapControllers();

app.Run();
