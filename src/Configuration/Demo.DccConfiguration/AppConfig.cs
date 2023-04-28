using Masa.Contrib.Configuration;
using Masa.Contrib.Configuration.ConfigurationApi.Dcc.Options;
using System.Collections;

namespace Demo.DccConfiguration;

public class AppConfig : DccConfigurationOptions
{
    public List<string> PositionTypes { get; set; }

    public JWTConfig JWTConfig { get; set; }
}

public class JWTConfig
{
    public string Issuer { get; set; }
    public string SecretKey { get; set; }
    public string Audience { get; set; }
}
