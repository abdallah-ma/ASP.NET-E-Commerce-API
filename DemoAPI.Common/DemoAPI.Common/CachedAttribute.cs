using DemoAPI.Common.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text;



namespace DemoAPI.Common
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        public int LifeSpan { get; set; }

        public CachedAttribute(int lifeSpan)
        {
            LifeSpan = lifeSpan;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var _cashService = context.HttpContext.RequestServices.GetService(typeof(ICachingService)) as ICachingService;

            var CacheKey = GenerateCacheKey(context);

            var Response = await _cashService.GetCachedResponse(CacheKey);

            if (!String.IsNullOrEmpty(Response))
            {
                context.Result = new ContentResult()
                {
                    Content = Response,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                return;
            }
            var executedContext = await next.Invoke();
            if (executedContext.Result is ObjectResult objectResult && objectResult.Value is not null)
            {
                await _cashService.CacheResponse(CacheKey, objectResult.Value, TimeSpan.FromMinutes(5));
            }

        }

        private string GenerateCacheKey(ActionExecutingContext context)
        {
            var Response = new StringBuilder();

            Response.Append(context.HttpContext.Request.Path);

            foreach (var pair in context.HttpContext.Request.Query)
            {
                Response.Append($"|{pair.Key}={pair.Value}");
            }
            return Response.ToString();
        }
    }
}
