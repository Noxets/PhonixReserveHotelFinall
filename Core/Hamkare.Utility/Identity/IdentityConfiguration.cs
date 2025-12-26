using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hamkare.Utility.Identity;

public static class IdentityConfiguration
{
    public static void BaseIdentity<TUser, TRole, TApplicationDbContext>(this IServiceCollection serviceCollection)
        where TRole : class where TUser : class where TApplicationDbContext : DbContext
    {
        serviceCollection.Configure<DataProtectionTokenProviderOptions>(o =>
        {
            o.TokenLifespan = TimeSpan.FromSeconds(90);
        });

        serviceCollection.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.LoginPath = "/Login";
                options.Cookie.Expiration = TimeSpan.FromDays(365);
                options.ExpireTimeSpan = TimeSpan.FromDays(8);
                options.AccessDeniedPath = "/AccessDenied";
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });

        serviceCollection.AddIdentity<TUser, TRole>(identityOptions =>
            {
                identityOptions.Password.RequireDigit = true;
                identityOptions.Password.RequireLowercase = true;
                identityOptions.Password.RequireNonAlphanumeric = false;
                identityOptions.Password.RequireUppercase = true;
                identityOptions.Password.RequiredLength = 8;
                identityOptions.User.RequireUniqueEmail = false;
                identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                identityOptions.Lockout.MaxFailedAccessAttempts = 10;
                identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                identityOptions.Lockout.AllowedForNewUsers = true;
            }).AddRoles<TRole>()
            .AddEntityFrameworkStores<TApplicationDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();
    }
}