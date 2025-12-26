using System.Globalization;
using System.Net;
using Hamkare.Utility.Configurations.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Hamkare.Utility.Configurations.ConfigurationService;

public static class MiddlewareConfiguration
{
    public static void BaseMiddleware(this WebApplication webApplication)
    {
        if (!webApplication.Environment.IsDevelopment())
        {
            webApplication.UseExceptionHandler("/Error", createScopeForErrors: true);
            webApplication.UseHsts();
            webApplication.UseResponseCompression();
        }
        else
        {
            webApplication.UseDeveloperExceptionPage();
        }

        webApplication.UseStaticFiles();
        webApplication.UseStaticFiles(new StaticFileOptions
        {
            OnPrepareResponse = context => {
                context.Context.Response.Headers.Append("Cache-Control", $"public, max-age={(60 * 60 * 24 * 7).ToString()}");
                context.Context.Response.Headers.Append("Expires", DateTime.UtcNow.AddDays(7).ToString(CultureInfo.InvariantCulture));
            }
        });

        webApplication.UseStatusCodePages(context => {
            var response = context.HttpContext.Response;

            if (response.StatusCode == (int) HttpStatusCode.Unauthorized)
                response.Redirect("/Login");

            if (response.StatusCode == (int) HttpStatusCode.NotFound)
                response.Redirect("/NotFound");

            if (response.StatusCode == (int) HttpStatusCode.Forbidden)
                response.Redirect("/AccessDenied");
            return Task.CompletedTask;
        });
        
            // webApplication.UseMiddleware<LicenseMiddleware>();
        webApplication.UseSession();
        webApplication.UseRouting();
        webApplication.UseCors();
        webApplication.UseAuthentication();
        webApplication.UseAuthorization();
        webApplication.UseRewriter(new RewriteOptions().AddRedirectToNonWww());
        webApplication.UseAntiforgery();
    }

    public static void CustomMiddleware(this WebApplication webApplication)
    {
        webApplication.UseRequestLocalization(webApplication.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
        webApplication.UseMiddleware<ExceptionMiddleware>();
    }
}