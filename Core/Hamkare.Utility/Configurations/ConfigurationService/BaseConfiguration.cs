using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using MudBlazor.Services;

namespace Hamkare.Utility.Configurations.ConfigurationService;

public static class BaseConfiguration
{
    public static void Base(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            }).AddViewLocalization()
            .AddDataAnnotationsLocalization();

        serviceCollection.AddRazorPages();

        serviceCollection.AddRazorComponents()
            .AddInteractiveServerComponents().AddCircuitOptions(options =>
            {
                options.DetailedErrors = true;
                options.DisconnectedCircuitRetentionPeriod = TimeSpan.FromMilliseconds(100);
                options.DisconnectedCircuitMaxRetained = 600;
                options.JSInteropDefaultCallTimeout = TimeSpan.FromMinutes(1);
                options.MaxBufferedUnacknowledgedRenderBatches = 10;
            })
            .AddHubOptions(options =>
            {
                options.ClientTimeoutInterval = TimeSpan.FromHours(16);
                options.HandshakeTimeout = TimeSpan.FromHours(8);
                options.MaximumReceiveMessageSize = 32 * 1024 * 1024;
            });

        serviceCollection.AddSignalR();

        serviceCollection.AddCors(x =>
            x.AddDefaultPolicy(policyBuilder => policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

        serviceCollection.AddMudServices(config =>
        {
            config.SnackbarConfiguration.PositionClass =
                Thread.CurrentThread.CurrentUICulture.Name == "fa" || Thread.CurrentThread.CurrentUICulture.Name == "ar"
                    ? "Bottom-Right"
                    : "Bottom-Left";
            config.SnackbarConfiguration.ShowCloseIcon = true;
            config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
        });
        serviceCollection.AddMudBlazorDialog();
        serviceCollection.AddMudBlazorResizeListener();

        serviceCollection.AddDistributedMemoryCache();
        serviceCollection.AddSession(options => { options.IdleTimeout = TimeSpan.FromHours(8); });

        serviceCollection.AddTransient<HttpClient>();
        serviceCollection.AddHttpContextAccessor();

        serviceCollection.AddCascadingAuthenticationState();
    }
}