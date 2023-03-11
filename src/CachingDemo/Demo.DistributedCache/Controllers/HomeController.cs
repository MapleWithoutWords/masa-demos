using Masa.BuildingBlocks.Caching;
using Microsoft.AspNetCore.Mvc;

namespace Demo.DistributedCache.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly IDistributedCacheClient _distributedCacheClient;
        private readonly IDistributedCacheClientFactory _distributedCacheClientFactory;

        public HomeController(IDistributedCacheClient distributedCacheClient, IDistributedCacheClientFactory distributedCacheClientFactory)
        {
            _distributedCacheClient = distributedCacheClient;
            _distributedCacheClientFactory = distributedCacheClientFactory;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            var cacheList = await _distributedCacheClient.GetAsync<string[]>(nameof(Summaries));
            if (cacheList != null)
            {
                Console.WriteLine($"从缓存中获取数据：【{string.Join(",", cacheList)}】");
                return cacheList;
            }

            Console.WriteLine($"写入数据到缓存");
            await _distributedCacheClient.SetAsync(nameof(Summaries), Summaries);
            return Summaries;
        }

        [HttpGet("factory")]
        public async Task<IEnumerable<string>> GetByFactory()
        {
            var distributedCacheClient = _distributedCacheClientFactory.Create();

            var cacheList = await distributedCacheClient.GetAsync<string[]>(nameof(Summaries));
            if (cacheList != null)
            {
                Console.WriteLine($"从缓存中获取数据：【{string.Join(",", cacheList)}】");
                return cacheList;
            }

            Console.WriteLine($"写入数据到缓存");
            await distributedCacheClient.SetAsync(nameof(Summaries), Summaries);
            return Summaries;
        }
    }
}