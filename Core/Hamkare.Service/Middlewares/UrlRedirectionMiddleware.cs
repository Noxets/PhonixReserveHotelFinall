using Hamkare.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Hamkare.Service.Middlewares;

public class UrlRedirectionMiddleware(RequestDelegate next, IDbContextFactory<ApplicationDbContext> dbcontext)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var redirection = (await dbcontext.CreateDbContextAsync()).Redirects.AsNoTracking().FirstOrDefault(x => x.Source.ToUpper() == context.Request.Path.ToString().ToUpper());
        if (redirection != null)
        {
            context.Response.StatusCode = (int)redirection.Code;
            context.Response.Redirect(redirection.Destination);
            return;
        }

        await next(context);
    }
}