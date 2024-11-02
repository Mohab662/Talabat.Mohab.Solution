namespace Talabat.Core.Services.Contract
{
    public interface IResponseCacheService
    {
        Task CacheResponseAsync(string cacheKey, object response, TimeSpan expireTime);

        Task<string?> GetCachedResponseAsync(string cacheKey);
    }
}
