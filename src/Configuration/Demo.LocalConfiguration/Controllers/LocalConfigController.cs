using Demo.LocalConfiguration.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Demo.LocalConfiguration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalConfigController : ControllerBase
    {
        private readonly IOptions<PositionTypeOptions> _positionTypeOptions;

        public LocalConfigController(IOptions<PositionTypeOptions> positionTypeOptions)
        {
            _positionTypeOptions = positionTypeOptions;
        }

        [HttpGet]
        public List<string> GetStrings()
        {
            return _positionTypeOptions.Value.PositionTypes;
        }

    }
}
