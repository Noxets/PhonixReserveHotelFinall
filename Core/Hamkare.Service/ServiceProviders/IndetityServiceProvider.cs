using Hamkare.Common.Interface.Services.Identity;
using Hamkare.Service.Services.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Hamkare.Service.ServiceProviders;

internal static class IdentityServiceProvider
{
    internal static void Identity(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<UserService>();
        serviceCollection.AddTransient<UserCategoryService>();
        serviceCollection.AddTransient<RoleService>();
        serviceCollection.AddTransient<ITokenService, TokenService>();
    }
}