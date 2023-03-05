using Demo.WebApi.Domain.AuthManager;
using Demo.WebApi.Domain.AuthManager.Entities;
using Demo.WebApi.Infrastructures;
using Masa.BuildingBlocks.Caching;
using Masa.BuildingBlocks.Data.UoW;
using Masa.BuildingBlocks.Ddd.Domain.Repositories;
using Masa.BuildingBlocks.Dispatcher.Events;
using Masa.BuildingBlocks.Dispatcher.IntegrationEvents;
using Masa.BuildingBlocks.Dispatcher.IntegrationEvents.Logs;
using Masa.Contrib.Dispatcher.IntegrationEvents;
using Masa.Contrib.Dispatcher.IntegrationEvents.EventLogs.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.VisualBasic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

builder.Services.AddMultilevelCache(opt => opt.UseStackExchangeRedisCache());

builder.Services.AddEndpointsApiExplorer()
    .AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Masa EShop - Catalog HTTP API",
            Version = "v1",
            Description = "The Catalog Service HTTP API"
        });
    }).AddMasaMinimalAPIs();


builder.Services.AddDomainEventBus(options =>
{
    options.UseIntegrationEventBus<IntegrationEventLogService>(opt => opt.UseDapr().UseEventLog<DemoDbContext>())
           .UseEventBus()
           .UseUoW<DemoDbContext>(dbOptions => dbOptions.UseMySQL("Server=127.0.0.1;Port=6031;database=ms_demo;uid=root;pwd=lfm@123;SslMode=None;Pooling=true;Max Pool Size=200;Allow User Variables=true;"))
           .UseRepository<DemoDbContext>();
});

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DemoDbContext>();
    //context.Database.Migrate();


    if (!context.Roles.Any())
    {
        IEnumerable<RoleEntity> GetRoleList()
        {
            return new List<RoleEntity>()
            {
                new RoleEntity () { Name="Admin", Remark=""},
            };
        }

        context.Roles.AddRange(GetRoleList());

        context.SaveChanges();
    }
}
app.UseSwagger().UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Masa EShop Service HTTP API v1");
});

app.MapMasaMinimalAPIs();

app.UseRouting();
app.UseCloudEvents();
app.UseEndpoints(endpoint =>
{
    endpoint.MapSubscribeHandler();
});

app.Run();
