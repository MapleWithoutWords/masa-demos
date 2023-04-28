using Masa.BuildingBlocks.Service.Caller;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCaller(clientBuilder =>
{
    clientBuilder.UseHttpClient(httpClient =>
    {
        httpClient.BaseAddress = "https://ServiceAddress"; //指定API服务域名地址
        httpClient.Prefix = "api/users";//指定API服务前缀

        httpClient.UseXml();//使用Xml请求
    });
});


var app = builder.Build();

app.MapControllers();

app.Run();
