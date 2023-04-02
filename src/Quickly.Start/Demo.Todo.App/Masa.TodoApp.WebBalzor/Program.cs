using Masa.TodoApp.WebBalzor.ApiCallers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddMasaBlazor();
builder.Services.Configure<TodoServiceOptions>(builder.Configuration.GetSection("TodoService"))
    .AddAutoRegistrationCaller(typeof(Program).Assembly);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
