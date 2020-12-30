using Microsoft.AspNetCore.Builder;
using Restaurant.Backend.Common.Middleware;

namespace Restaurant.Backend.Common.Extensions
{
    public static class ExceptionHandlerExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
