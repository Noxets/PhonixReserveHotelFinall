using Microsoft.AspNetCore.Builder;

namespace Hamkare.Utility.Configurations.ConfigurationService;

public static class RouteConfiguration
{
    public static void BaseRoute(this WebApplication webApplication)
    {
        webApplication.MapControllerRoute(
            "default",
            "{controller=Home}/{action=Index}/{id?}");
        
        webApplication.MapControllerRoute(
            name: "home",
            pattern: "{action=Index}/{id?}",
            defaults: new
            {
                controller = "Home"
            });

        webApplication.MapAreaControllerRoute(
            "Api",
            // ReSharper disable once Mvc.AreaNotResolved
            "Api",
            "Api/{controller=Home}/{action=Index}/{id?}");
        
        webApplication.UseBlazorFrameworkFiles();
    }
}