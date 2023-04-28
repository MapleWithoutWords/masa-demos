using Masa.BuildingBlocks.Service.Caller;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAutoRegistrationCaller(typeof(Program).Assembly);

builder.Services.AddCaller(clientBuilder =>
{
    clientBuilder.UseDapr(client =>
    {
        client.AppId = "{Replace-With-Your-Dapr-AppID}";
        client.UseXml();
    });
});

var app = builder.Build();

app.MapControllers();

app.Run();
