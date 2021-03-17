using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Primitives;
using System;
using System.Threading.Tasks;

namespace WebApiKey.Support
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKey : Attribute, IAsyncActionFilter
    {

        public const string Key = "api_key";

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

            if (!await HashKeyVerifyAsync(value))
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

        private async Task<bool> HashKeyVerifyAsync(string value)
        {
            int count = 0;
            using SqliteConnection connection = new(DatabasePath.Path);
            using SqliteCommand command = new ("SELECT COUNT(*) as count FROM keys WHERE hash=@hash", connection);
            command.Parameters.Add("@hash", SqliteType.Text, 100).Value = value;
            await connection.OpenAsync();
            using SqliteDataReader reader = await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                await reader.ReadAsync();
                count = reader.GetInt32(0);
            }
            await reader.DisposeAsync();
            await command.DisposeAsync();
            await connection.DisposeAsync();
            return (count == 1);
        }
    }
}
