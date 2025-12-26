using Hamkare.Service.Services.General;
using Microsoft.Extensions.DependencyInjection;

namespace Hamkare.Service.ServiceProviders;

public static class GeneralServiceProvider
{
    internal static void General(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<RedirectService>();
        serviceCollection.AddTransient<SettingService>();
    }
}