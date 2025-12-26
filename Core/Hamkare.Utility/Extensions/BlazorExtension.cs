using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;

namespace Hamkare.Utility.Extensions;

public static class BlazorExtension
{
    public static async Task LogError(this IJSRuntime runtime, Exception exception)
    {
        await runtime.InvokeVoidAsync("console.error", exception.Message);
        await runtime.InvokeVoidAsync("console.error", exception.StackTrace);
    }

    public static async Task<string> GetUser(this AuthenticationStateProvider authenticationStateProvider)
    {
        var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        return user.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}