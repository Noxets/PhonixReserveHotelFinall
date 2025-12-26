using Hamkare.Common.Entities.Identity;
using Hamkare.Common.Interface.Repositories;
using Hamkare.Infrastructure;
using Hamkare.Service.Services.Generic;

namespace Hamkare.Service.Services.Identity;

public class UserCategoryService(ICategoryRepository<UserCategory, ApplicationDbContext> repository) : CategoryService<UserCategory, ApplicationDbContext>(repository)
{
    public async Task<List<long>> GetChildId(long parentId, CancellationToken cancellationToken = default)
    {
        var newsGroup = await GetAllAsync(x => x.ParentId == parentId, cancellationToken);
        var result = newsGroup.Select(x => x.Id).ToList();

        foreach (var item in newsGroup)
            result.AddRange(await GetChildId(item.Id, cancellationToken));

        return result;
    }

    public async Task<IReadOnlyList<UserCategory>> AvailableParent(long productGroupId, CancellationToken cancellationToken = default)
    {
        var child = await GetChildId(productGroupId, cancellationToken);

        return await GetAllAsync(x => !child.Contains(x.Id) && x.Id != productGroupId, cancellationToken);
    }
}