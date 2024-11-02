using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using Talabat.Core.Services.Contract;

namespace Talabat.APIS.Helpers
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _expireTimeInSeconds;

        public CachedAttribute(int expireTimeInSeconds)
        {
            _expireTimeInSeconds = expireTimeInSeconds;

        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();

            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
            var cacheResponse = await cacheService.GetCachedResponseAsync(cacheKey);

            if (!string.IsNullOrEmpty(cacheResponse))
            {
                var contentResult = new ContentResult() 
                {
                    Content=cacheResponse,
                    ContentType="application/json",
                    StatusCode=200
                };
                context.Result = contentResult;
                return;
            }

            var excutedEndPointContext=await next.Invoke();

            if (excutedEndPointContext.Result is OkObjectResult result)
            {
               await cacheService.CacheResponseAsync(cacheKey, result.Value, TimeSpan.FromSeconds(_expireTimeInSeconds));
            }
        }

        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var KeyBuilder = new StringBuilder();

            KeyBuilder.Append(request.Path);
            foreach (var (key, value) in request.Query.OrderBy(o => o.Key))
            {
                KeyBuilder.Append($"|{key}-{value}");

            }
            return KeyBuilder.ToString();
        }
    }
}
