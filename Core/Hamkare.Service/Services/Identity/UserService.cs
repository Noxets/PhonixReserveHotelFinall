using Hamkare.Common.Entities.Identity;
using Hamkare.Common.Interface.Repositories;
using Hamkare.Infrastructure;
using Hamkare.Service.Services.Generic;

namespace Hamkare.Service.Services.Identity;

public class UserService(IRootRepository<User, ApplicationDbContext> repository)
    : RootService<User, ApplicationDbContext>(repository)
{
    public async Task<User> GetRegisterUserByPhoneNumber(string phoneNumber,
        CancellationToken cancellationToken = default)
    {
        return await GetAsync(x => x.PhoneNumber == phoneNumber, cancellationToken);
    }

    public async Task<User> GetLoginUserByPhoneNumber(string phoneNumber, CancellationToken cancellationToken = default)
    {
        return await GetActiveAsync(x => x.PhoneNumber == phoneNumber && (!x.LockoutEnabled), cancellationToken);
    }

    public async Task<bool> CanLoginUserByPhoneNumber(string phoneNumber, CancellationToken cancellationToken = default)
    {
        return await GetAnyActiveAsync(x => x.PhoneNumber == phoneNumber && (!x.LockoutEnabled), cancellationToken);
    }

    public async Task<User> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        if (long.TryParse(id, out long parsedId))
            return await FindAsync(parsedId, cancellationToken);

        return null;
    }
}