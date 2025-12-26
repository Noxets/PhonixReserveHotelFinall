using Hamkare.Common.Entities.News;
using Hamkare.Common.Interface.Repositories;
using Hamkare.Infrastructure;
using Hamkare.Service.Services.Generic;

namespace Hamkare.Service.Services.News;

public class NewsCategoryService(ICategoryRepository<NewsCategory, ApplicationDbContext> repository) : CategoryService<NewsCategory, ApplicationDbContext>(repository)
{
    public async Task<List<long>> GetChildId(long parentId, CancellationToken cancellationToken = default)
    {
        var newsGroup = await GetAllAsync(x => x.ParentId == parentId, cancellationToken);
        var result = newsGroup.Select(x => x.Id).ToList();

        foreach (var item in newsGroup)
            result.AddRange(await GetChildId(item.Id, cancellationToken));

        return result;
    }

    public async Task<IReadOnlyList<NewsCategory>> AvailableParent(long newsgroupId, CancellationToken cancellationToken = default)
    {
        var child = await GetChildId(newsgroupId, cancellationToken);

        return await GetAllAsync(x => !child.Contains(x.Id) && x.Id != newsgroupId, cancellationToken);
    }
}