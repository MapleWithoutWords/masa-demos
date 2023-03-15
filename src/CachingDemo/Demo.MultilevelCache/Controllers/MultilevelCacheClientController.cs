using Masa.BuildingBlocks.Caching;
using Masa.Contrib.Caching.MultilevelCache;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.MultilevelCache.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MultilevelCacheClientController : ControllerBase
    {
        const string key = "MultilevelCacheTest";
        private readonly IMultilevelCacheClient _multilevelCacheClient;
        public MultilevelCacheClientController(IMultilevelCacheClient multilevelCacheClient) => _multilevelCacheClient = multilevelCacheClient;

        [HttpGet]
        public async Task<string?> GetAsync()
        {
            var cacheValue = await _multilevelCacheClient.GetAsync<string>(key, val => { Console.WriteLine($"值被改变了：{val}"); }, null);
            if (cacheValue != null)
            {
                Console.WriteLine($"get data by multilevel cahce：【{cacheValue}】");
                return cacheValue;
            }
            cacheValue = "multilevelClient";
            Console.WriteLine($"use factory write data【{cacheValue}】to multilevel cache");
            await _multilevelCacheClient.SetAsync(key, cacheValue);
            return cacheValue;
        }

        [HttpPost]
        public async Task<string?> SetAsync(string value = "multilevelClient")
        {
            Console.WriteLine($"use factory write data【{value}】to multilevel cache");
            await _multilevelCacheClient.SetAsync(key, value);
            return value;
        }

        [HttpDelete]
        public async Task RemoveAsync()
        {
            await _multilevelCacheClient.RemoveAsync<string>(key);
        }
    }
}
