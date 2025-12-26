using Microsoft.AspNetCore.Http;
using System.Net;

namespace Hamkare.Utility.Configurations.Middlewares;

public class ExceptionMiddleware(RequestDelegate next)
{

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
        await context.Response.WriteAsync(new
        {
            statusCode = context.Response.StatusCode,
            message = exception.Message,
            success = false
        }.ToString() ?? string.Empty);
    }
}