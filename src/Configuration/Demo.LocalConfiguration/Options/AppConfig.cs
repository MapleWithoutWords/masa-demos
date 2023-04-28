using Masa.Contrib.Configuration;
using System.Collections;

namespace Demo.LocalConfiguration.Options;

public class AppConfig : LocalMasaConfigurationOptions
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
