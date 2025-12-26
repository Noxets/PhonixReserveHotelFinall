using Hamkare.Common.Entities.Identity;
using Hamkare.Demo.Components;
using Hamkare.Infrastructure;
using Hamkare.Panel.Components;
using Microsoft.EntityFrameworkCore;
using Hamkare.Service;
using Hamkare.Service.Middlewares;
using Hamkare.Service.Services.General;
using Hamkare.Utility.Configurations.ConfigurationService;
using Hamkare.Utility.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Base();

builder.Services.AddDbContextFactory<ApplicationDbContext>(c => {
    c.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
}
);

builder.Services.BaseRepository();
builder.Services.BaseService();
builder.Services.BaseCompression();
builder.Services.BaseIdentity<User, Role, ApplicationDbContext>();

var app = builder.Build();

app.CustomMiddleware();
app.UseMiddleware<UrlRedirectionMiddleware>();

app.BaseMiddleware();

app.BaseRoute();

using (var scope = app.Services.CreateScope())
{
    var settingService = scope.ServiceProvider.GetRequiredService<SettingService>();
    await settingService.UpdateSetting();
}

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(Hamkare.AdminPanel.Components._Imports).Assembly)
    .AddAdditionalAssemblies(typeof(Imports).Assembly);

app.Run();