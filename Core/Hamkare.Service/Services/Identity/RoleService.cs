using Hamkare.Common.Entities.Identity;
using Hamkare.Common.Interface.Repositories;
using Hamkare.Infrastructure;
using Hamkare.Service.Services.Generic;
using Microsoft.AspNetCore.Identity;

namespace Hamkare.Service.Services.Identity;

public class RoleService(IRootRepository<Role, ApplicationDbContext> repository, UserManager<User> userManager) : RootService<Role, ApplicationDbContext>(repository)
{
    public async Task<bool> HasAccessToPanel(User user, string panelName)
    {
        var roles = await userManager.GetRolesAsync(user);

        var roleList = await GetAllActiveAsync(x => roles.Contains(x.Name));

        return roleList.Any(x => x.ReturnUrl.Contains(panelName));
    }

    public async Task<bool> HasRole(User user, string roleName)
    {
        var roles = await userManager.GetRolesAsync(user);

        return roles.Any(x => x.Contains(roleName));
    }

    public async Task<IList<string>> HasRole(User user)
    {
        var roles = await userManager.GetRolesAsync(user);

        return roles;
    }

    public async Task<bool> HasRole(User user, params string[] roleName)
    {
        var roles = await userManager.GetRolesAsync(user);

        return roles.Any(roleName.Contains);
    }
}