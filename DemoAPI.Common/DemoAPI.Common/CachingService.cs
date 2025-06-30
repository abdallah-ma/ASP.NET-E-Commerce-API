using DemoAPI.Common;
using DemoAPI.Common.Interfaces;
using StackExchange.Redis;
using System.Text.Json;
using IDatabase = StackExchange.Redis.IDatabase;

namespace DemoAPI.Common
{
    public class CachingService : ICachingService
    {


        public IDatabase _redis;

        public CachingService(IConnectionMultiplexer database)
        {
            _redis = database.GetDatabase();
        }


        public async Task CacheResponse(string Key,object Response,TimeSpan Lifespan)
        {

            if (Response == null) return;

            var json = JsonSerializer.Serialize(Response);

            await _redis.StringSetAsync(Key, json, Lifespan);

        }

        public async Task<string?> GetCachedResponse(string Key)
        {

            var Response = await _redis.StringGetAsync(Key);

            if(Response.IsNullOrEmpty)return null;

            return Response;

        }

    }
}
