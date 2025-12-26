using Hamkare.Common.Interface.Repositories;
using Hamkare.Infrastructure;
using Hamkare.Service.Services.Generic;

namespace Hamkare.Service.Services.News;

public class NewsService(IMainRepository<Hamkare.Common.Entities.News.News, ApplicationDbContext> repository, NewsCategoryService newsGroupService, NewsCommentService newsCommentService) : MainService<Hamkare.Common.Entities.News.News, ApplicationDbContext>(repository)
{
    private readonly IMainRepository<Hamkare.Common.Entities.News.News, ApplicationDbContext> _repository = repository;
    public async Task<IReadOnlyList<Hamkare.Common.Entities.News.News>> GetAllNewsInGroupAsync(long groupId, CancellationToken cancellationToken = default)
    {
        var groupIds = await newsGroupService.GetChildId(groupId, cancellationToken);

        var news = await GetAllAsync(x => groupIds.Contains((long) x.NewsGroupId), cancellationToken);

        return news;
    }

    public new async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var comments = await newsCommentService.GetAllAsync(x => x.NewsId == id, cancellationToken);
        foreach (var comment in comments)
            await newsCommentService.DeleteAsync(comment.Id, cancellationToken);

        var item = await FindAsync(id, cancellationToken);
        await _repository.Delete(item, cancellationToken);
    }
}