
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMasaMinimalAPIs();
builder.Services.AddEventBus();
builder.Services.AddDbContext<TodoDbContext>(opt => opt.UseSqlite());

var app = builder.Build();

app.MapMasaMinimalAPIs();

#region MigrationDb
var options = app.Services.GetService<IOptions<MasaDbContextOptions<TodoDbContext>>>();
using (var context = new TodoDbContext(options.Value))
{
	context.GetService<IRelationalDatabaseCreator>().CreateTables();
} 
#endregion

app.Run();
