using Masa.BuildingBlocks.Service.Caller;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddCaller(clientBuilder =>
{
    clientBuilder.UseHttpClient(httpClient =>
    {
        httpClient.BaseAddress = "https://UserService";//指定API服务域名地址
    }).UseAuthentication();
});

var app = builder.Build();


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
