using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//Swagger依赖Endpoint的一些服务，必须AddEndpointsApiExplorer，不然swagger不能使用
builder.Services.AddEndpointsApiExplorer()
    .AddSwaggerGen(c =>
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

builder.Services.AddMasaMinimalAPIs(opt =>
{
    var loadMinimalApiAssembies = opt.Assemblies.ToList();
    loadMinimalApiAssembies.Add(typeof(Program).Assembly); //业务层程序集
    opt.Assemblies = loadMinimalApiAssembies;
});

var app = builder.Build();

app.UseSwagger().UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test"));

app.MapMasaMinimalAPIs();

app.Run();
