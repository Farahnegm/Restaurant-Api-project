
using System.Diagnostics;

namespace Restaurants.Api.MiddleWares
{
    public class RequestTimeLoggingMiddleWare(ILogger<RequestTimeLoggingMiddleWare> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {

            var stop = Stopwatch.StartNew();
            await next.Invoke(context);
            stop.Stop();
            if (stop.ElapsedMilliseconds / 1000 > 4000) {
                logger.LogInformation("Request [{Verb}] at {Path} took {Time} ms",
                    context.Request.Method,
                    context.Request.Path,
                    stop.ElapsedMilliseconds
                    );

            }
        }
    }
}
