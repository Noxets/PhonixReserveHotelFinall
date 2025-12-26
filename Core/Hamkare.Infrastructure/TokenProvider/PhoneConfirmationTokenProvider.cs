using Hamkare.Common.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Hamkare.Infrastructure.TokenProvider;

public static class PhoneConfirmationTokenProvider
{
    public static async Task<string> GenerateAsync(this UserManager<User> manager, User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        var generator = new Random();
        var token = generator.Next(0, 1000000).ToString("D6");

        user.ActivationCode = token;
        user.ExpireActivationCode = DateTime.UtcNow.AddMinutes(2);
        await manager.UpdateAsync(user);

        return token;
    }

    public static async Task<bool> ValidateAsync(this UserManager<User> manager, User user, string token)
    {
        ArgumentNullException.ThrowIfNull(user);

        var result = user.ActivationCode == token && DateTime.UtcNow < user.ExpireActivationCode;

        if (!result)
            return false;

        user.ActivationCode = null;
        user.ExpireActivationCode = null;
        await manager.UpdateAsync(user);

        return true;
    }
}