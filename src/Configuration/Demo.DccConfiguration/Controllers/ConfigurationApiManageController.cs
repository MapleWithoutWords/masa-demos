using Masa.BuildingBlocks.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.DccConfiguration.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ConfigurationApiManageController : ControllerBase
{
    private readonly IConfigurationApiManage _configurationApiManage;

    public ConfigurationApiManageController(IConfigurationApiManage configurationApiManage)
    {
        _configurationApiManage = configurationApiManage;
    }

    [HttpPost]
    public async Task AddConfiguration()
    {
        var configObjectDic = new Dictionary<string, object> { };
        configObjectDic[nameof(AppConfig)] = new AppConfig
        {
            JWTConfig = new JWTConfig { Audience = "MASAStack.com" },
            PositionTypes = new List<string> { "MASA Stack" }
        };
        await _configurationApiManage.AddAsync("development enviroment", "default cluster", "application id", configObjectDic);
    }

    [HttpPut]
    public async Task UpdateConfiguration()
    {
        var configObject = new AppConfig
        {
            JWTConfig = new JWTConfig { Audience = "MASAStack.com" },
            PositionTypes = new List<string> { "MASA Stack" }
        };
        await _configurationApiManage.UpdateAsync("development enviroment", "default cluster", "application id", nameof(AppConfig), configObject);
    }
}
