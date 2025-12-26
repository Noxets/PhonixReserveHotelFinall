using Microsoft.AspNetCore.Identity;

namespace Hamkare.Common.Entities.Identity;

public static class IdentityExtension
{
    public static async Task<List<User>> GetUsersInRoleAsync(this UserManager<User> userManager, List<string> roles)
    {
        List<User> users = [];
        foreach (var role in roles)
            users.AddRange(await userManager.GetUsersInRoleAsync(role));

        return users;
    }
}