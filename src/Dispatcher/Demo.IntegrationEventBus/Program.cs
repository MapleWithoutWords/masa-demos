using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//Swagger����Endpoint��һЩ���񣬱���AddEndpointsApiExplorer����Ȼswagger����ʹ��
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
    loadMinimalApiAssembies.Add(typeof(Program).Assembly); //ҵ������
    opt.Assemblies = loadMinimalApiAssembies;
});

var app = builder.Build();

app.UseSwagger().UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test"));

app.MapMasaMinimalAPIs();

app.Run();
