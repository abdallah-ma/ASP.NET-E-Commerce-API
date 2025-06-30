
namespace DemoAPI.Common.Interfaces
{
    public interface ICachingService
    {

        public Task CacheResponse(string Key, object Response, TimeSpan LifeSpan);

        public Task<String?> GetCachedResponse(string Key);

    }
}
