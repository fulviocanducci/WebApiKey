using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Threading.Tasks;

namespace WebApiKey.Support
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKey : Attribute, IAsyncActionFilter
    {

        public const string Key = "api_key";
        private const string KeyValue = "123456";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var request = context.HttpContext.Request;
            if (!request.Query.TryGetValue(Key, out StringValues value))
            {
                if (!request.Headers.TryGetValue(Key, out value))
                {
                    context.Result = new ContentResult()
                    {
                        StatusCode = 401,
                        Content = "ApiKey Unauthorized"
                    };
                    return;
                }
            }
            if (!value.Equals(KeyValue))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 403,
                    Content = "ApiKey Forbidden"
                };
                return;
            }
            await next();
        }
    }
}
