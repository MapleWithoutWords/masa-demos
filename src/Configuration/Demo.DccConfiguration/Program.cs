using Demo.DccConfiguration;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Test",
        Version = "v1",
        Contact = new OpenApiContact { Name = "Test", }
    });

    foreach (var item in Directory.GetFiles(Directory.GetCurrentDirectory(), "*.xml"))
    {
        c.IncludeXmlComments(item, true);
    }
    c.DocInclusionPredicate((docName, action) => true);
});
builder.Services.AddMasaConfiguration(configureBuilder =>
{
    configureBuilder.UseDcc();
    configureBuilder.UseMasaOptions(options =>
    {
        options.MappingConfigurationApi<AppConfig>("Dcc's Application Id", "App");
    });
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger().UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test"));

app.UseAuthorization();

app.MapControllers();

app.Run();
