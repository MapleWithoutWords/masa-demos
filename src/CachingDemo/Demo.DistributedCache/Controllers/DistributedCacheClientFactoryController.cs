using Masa.BuildingBlocks.Caching;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.DistributedCache.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistributedCacheClientFactoryController : ControllerBase
    {
        private static readonly string[] FactorySummaries = new[] { "FactoryData1", "FactoryData2", "FactoryData3" };
        private readonly IDistributedCacheClientFactory _distributedCacheClientFactory;

        public DistributedCacheClientFactoryController(IDistributedCacheClientFactory distributedCacheClientFactory) => _distributedCacheClientFactory = distributedCacheClientFactory;


        [HttpGet]
        public async Task<IEnumerable<string>> GetByFactory()
        {
            using (var distributedCacheClient = _distributedCacheClientFactory.Create())
            {
                var cacheList = await distributedCacheClient.GetAsync<string[]>(nameof(FactorySummaries));
                if (cacheList != null)
                {
                    Console.WriteLine($"使用工厂从缓存中获取数据：【{string.Join(",", cacheList)}】");
                    return cacheList;
                }

                Console.WriteLine($"使用工厂写入数据到缓存");
                await distributedCacheClient.SetAsync(nameof(FactorySummaries), FactorySummaries);
                return FactorySummaries;
            }
        }
    }
}
