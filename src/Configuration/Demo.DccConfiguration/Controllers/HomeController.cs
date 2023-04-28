using Masa.BuildingBlocks.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Demo.DccConfiguration.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HomeController : ControllerBase
{
    private readonly IMasaConfiguration _masaConfiguration;
    private readonly IConfigurationApiClient _configurationApiClient;

    public HomeController(IMasaConfiguration masaConfiguration, IConfigurationApiClient configurationApiClient)
    {
        _masaConfiguration = masaConfiguration;
        _configurationApiClient = configurationApiClient;
    }
    [HttpGet]
    public async Task<AppConfig> GetAppConfig()
    {
        return await _configurationApiClient.GetAsync<AppConfig>("enviroment", "cluster", "appId", "AppConfig", newvalue =>
        {
            Console.WriteLine("值发生了改变");
        });
    }

    [HttpGet]
    public string? GetStrings()
    {
        return _masaConfiguration.ConfigurationApi.Get("Dcc's Application Id").GetSection("Local:App:JWTConfig:Issuer")?.Value;
    }
}
