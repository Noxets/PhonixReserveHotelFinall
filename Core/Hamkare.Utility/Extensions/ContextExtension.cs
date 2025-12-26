using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hamkare.Utility.Extensions;

public static class ContextExtension
{
    public static string GetLocalUrl(this IUrlHelper urlHelper, string localUrl)
    {
        if (!urlHelper.IsLocalUrl(localUrl))
        {
            return urlHelper.Page("/Index");
        }

        return localUrl;
    }

    public static string EmailConfirmationLink(this IUrlHelper urlHelper, string userId, string code, string scheme)
    {
        return urlHelper.Page(
        "/Account/ConfirmEmail",
        null,
        new
        {
            userId,
            code
        },
        scheme);
    }

    public static string ResetPasswordCallbackLink(this IUrlHelper urlHelper, string userId, string code, string scheme)
    {
        return urlHelper.Page(
        "/Account/ResetPassword",
        null,
        new
        {
            userId,
            code
        },
        scheme);
    }

    public static bool IsDesktop(this HttpRequest httpRequest)
    {
        return httpRequest.Headers.UserAgent.ToString().Contains("Windows");
    }

    public static bool IsDesktop(this HttpContext httpContext)
    {
        return httpContext.Request.Headers.UserAgent.ToString().Contains("Windows");
    }
}