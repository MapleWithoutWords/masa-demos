using Demo.LocalConfiguration.Options;
using Masa.BuildingBlocks.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Demo.LocalConfiguration.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HomeController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public HomeController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public string? GetStrings()
    {
        return _configuration.GetSection("Local:App:JWTConfig:Issuer")?.Value;
    }
}
