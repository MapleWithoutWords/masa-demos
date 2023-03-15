using Masa.BuildingBlocks.Caching;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.MultilevelCache.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MultilevelCacheClientFactoryController : ControllerBase
    {
        const string key = "MultilevelCacheFactoryTest";
        private readonly IMultilevelCacheClientFactory _multilevelCacheClientFactory;
        public MultilevelCacheClientFactoryController(IMultilevelCacheClientFactory multilevelCacheClientFactory) => _multilevelCacheClientFactory = multilevelCacheClientFactory;

        [HttpGet]
        public async Task<string?> TestAsync(string value= "MultilevelCacheFactoryValue")
        {
            using var multilevelCacheClient = _multilevelCacheClientFactory.Create();
            var cacheValue = await multilevelCacheClient.GetAsync<string>(key);
            if (cacheValue != null)
            {
                Console.WriteLine($"use factory get data by multilevel cache：【{cacheValue}】");
                return cacheValue;
            }
            cacheValue = value;
            Console.WriteLine($"use factory write data【{cacheValue}】to multilevel cache");
            await multilevelCacheClient.SetAsync(key, cacheValue);
            return cacheValue;
        }
    }
}
