﻿
using Restaurants.Domain.Exceptions;

namespace Restaurants.Api.MiddleWare
{
    public class ErrorHandlingMiddleWare(ILogger<ErrorHandlingMiddleWare> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (NotFoundException notFound)
            {
         
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFound.Message);
                logger.LogWarning(notFound.Message);
            }
            catch (forbidException)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Access forbidden");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something Went Wrong");
            }
        }
    }
}