using WebApplication1;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ServiceCallerOptions>(builder.Configuration.GetSection("ServiceCaller"));
builder.Services.AddAutoRegistrationCaller(typeof(Program).Assembly);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
