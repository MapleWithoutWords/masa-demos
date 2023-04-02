using Masa.Contrib.Configuration;
using System.Collections;

namespace Demo.LocalConfiguration.Options
{
    public class PositionTypeOptions : LocalMasaConfigurationOptions
    {
        public List<string> PositionTypes { get; set; }
    }
}
