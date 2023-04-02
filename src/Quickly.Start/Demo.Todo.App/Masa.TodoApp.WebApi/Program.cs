using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Masa.TodoApp.WebApi.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEventBus()
    .AddMasaDbContext<TodoDbContext>(opt => opt.UseSqlite())
    .AddMasaMinimalAPIs(option => option.MapHttpMethodsForUnmatched = new string[] { "Post" })
    .AddAutoInject();

//Swagger依赖Endpoint的一些服务，必须AddEndpointsApiExplorer，不然swagger不能使用
builder.Services.AddEndpointsApiExplorer()
    .AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "TodoApp", Version = "v1", Contact = new OpenApiContact { Name = "TodoApp", } });
        foreach (var item in Directory.GetFiles(Directory.GetCurrentDirectory(), "*.xml"))
            c.IncludeXmlComments(item, true);
        c.DocInclusionPredicate((docName, action) => true);
    });

var app = builder.Build();

app.UseMasaExceptionHandler();
app.MapMasaMinimalAPIs();
#region MigrationDb
using var context = app.Services.CreateScope().ServiceProvider.GetService<TodoDbContext>();
{
    if (context!.GetService<IRelationalDatabaseCreator>().HasTables() == false)
    {
        context!.GetService<IRelationalDatabaseCreator>().CreateTables();
    }
}
#endregion
if (app.Environment.IsDevelopment())
    app.UseSwagger().UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoApp"));
app.Run();