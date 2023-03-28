var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEventBus();
builder.Services.AddMasaDbContext<TodoDbContext>(opt => opt.UseSqlite());
builder.Services.AddMasaMinimalAPIs();

//Swagger依赖Endpoint的一些服务，必须AddEndpointsApiExplorer，不然swagger不能使用
builder.Services.AddEndpointsApiExplorer()
    .AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Todo",
            Version = "v1",
            Contact = new OpenApiContact { Name = "Todo", }
        });

        foreach (var item in Directory.GetFiles(Directory.GetCurrentDirectory(), "*.xml"))
        {
            c.IncludeXmlComments(item, true);
        }
        c.DocInclusionPredicate((docName, action) => true);
    });
builder.Services.AddAutoInject();

var app = builder.Build();

app.UseMasaExceptionHandler();
app.MapMasaMinimalAPIs();
#region MigrationDb
using var context = app.Services.CreateScope().ServiceProvider.GetService<TodoDbContext>();
{
    if (context.GetService<IRelationalDatabaseCreator>().HasTables()==false)
    {
        context.GetService<IRelationalDatabaseCreator>().CreateTables();
    }
}
#endregion

app.UseSwagger().UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo"));
app.Run();
