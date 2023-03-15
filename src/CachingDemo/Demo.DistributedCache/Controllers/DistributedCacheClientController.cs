using Masa.BuildingBlocks.Caching;
using Microsoft.AspNetCore.Mvc;

namespace Demo.DistributedCache.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DistributedCacheClientController : ControllerBase
    {
        private static readonly string[] Summaries = new[] { "Data1", "Data2", "Data3" };
        private readonly IDistributedCacheClient _distributedCacheClient;
        public DistributedCacheClientController(IDistributedCacheClient distributedCacheClient) => _distributedCacheClient = distributedCacheClient;

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
    }
}