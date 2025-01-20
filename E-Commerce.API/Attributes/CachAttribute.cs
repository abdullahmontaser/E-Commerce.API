using Azure.Core;
using E_commerce.core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace E_Commerce.API.Attributes
{
    public class CachAttribute: Attribute,IAsyncActionFilter
    {
        private readonly int _expirtTime;

        public CachAttribute(int expirtTime)
        {
            _expirtTime = expirtTime;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
           var cashService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>(); 
            var cachKey=GenrateCacheKeyFromRequest(context.HttpContext.Request);
            var cahcKeyResponse = await cashService.GetCacheKeyAsync(cachKey);
            if (!string.IsNullOrEmpty(cahcKeyResponse))
            {
                var contentResult = new ContentResult()
                {
                    Content = cahcKeyResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result = contentResult;
                return;
            }
           var executedContext= await next();
            if (executedContext.Result is OkObjectResult response)
            {
              await  cashService.SetCacheKeyAsync(cachKey, response.Value, TimeSpan.FromSeconds(_expirtTime));
            }
        }
        private string GenrateCacheKeyFromRequest(HttpRequest request) 
        { 
        var cacheKey = new StringBuilder();
            cacheKey.Append($"{request.Path}");
            foreach (var (key,value)  in request.Query.OrderBy(X=>X.Key)) 
            { 
            cacheKey.Append($"|{key}-{value}");
            }
            return cacheKey.ToString();
        }
    }
}
