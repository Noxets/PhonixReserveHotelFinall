using System.Linq.Expressions;
using Hamkare.Common.Interface.Entities;
using Hamkare.Common.Interface.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Hamkare.Infrastructure.Repository;

public class MainRepository<TMainEntity, TDbContext>(IDbContextFactory<TDbContext> context) : RootRepository<TMainEntity, TDbContext>(context), IMainRepository<TMainEntity, TDbContext>
    where TMainEntity : class, IMainEntity, new() where TDbContext : DbContext
{
    #region Get All

    public override async Task<IReadOnlyList<TMainEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await ExecuteCommand(() => GetAllAsyncCommand(cancellationToken));
    }
    private async Task<IReadOnlyList<TMainEntity>> GetAllAsyncCommand(CancellationToken cancellationToken = default)
    {
        return await Context.Set<TMainEntity>().Where(x => !x.Deleted).OrderBy(x => x.Order).ThenByDescending(x => x.Id).ToListAsync(cancellationToken);
    }

    public override async Task<IReadOnlyList<TMainEntity>> GetAllAsync(int top, int skip, CancellationToken cancellationToken = default)
    {
        return await ExecuteCommand(() => GetAllAsyncCommand(top, skip, cancellationToken));
    }
    private async Task<IReadOnlyList<TMainEntity>> GetAllAsyncCommand(int top, int skip, CancellationToken cancellationToken = default)
    {
        return await Context.Set<TMainEntity>().Where(x => !x.Deleted).OrderBy(x => x.Order).ThenByDescending(x => x.Id).Take(top).Skip(skip).ToListAsync(cancellationToken);
    }

    public override async Task<IReadOnlyList<TMainEntity>> GetAllAsync(Expression<Func<TMainEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await ExecuteCommand(() => GetAllAsyncCommand(expression, cancellationToken));
    }
    private async Task<IReadOnlyList<TMainEntity>> GetAllAsyncCommand(Expression<Func<TMainEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await Context.Set<TMainEntity>().Where(expression).Where(x => !x.Deleted).OrderBy(x => x.Order).ThenByDescending(x => x.Id).ToListAsync(cancellationToken);
    }

    public override async Task<IReadOnlyList<TMainEntity>> GetAllAsync(Expression<Func<TMainEntity, bool>> expression, int top, int skip, CancellationToken cancellationToken = default)
    {
        return await ExecuteCommand(() => GetAllAsyncCommand(expression, top, skip, cancellationToken));
    }
    private async Task<IReadOnlyList<TMainEntity>> GetAllAsyncCommand(Expression<Func<TMainEntity, bool>> expression, int top, int skip, CancellationToken cancellationToken = default)
    {
        return await Context.Set<TMainEntity>().Where(expression).Where(x => !x.Deleted).OrderBy(x => x.Order).ThenByDescending(x => x.Id).Take(top).Skip(skip).ToListAsync(cancellationToken);
    }

    #endregion

    #region Get All Active

    public override async Task<IReadOnlyList<TMainEntity>> GetAllActiveAsync(CancellationToken cancellationToken = default)
    {
        return await ExecuteCommand(() => GetAllActiveAsyncCommand(cancellationToken));
    }
    private async Task<IReadOnlyList<TMainEntity>> GetAllActiveAsyncCommand(CancellationToken cancellationToken = default)
    {
        return await Context.Set<TMainEntity>().Where(x => !x.Deleted && x.Active).OrderBy(x => x.Order).ThenByDescending(x => x.Id).ToListAsync(cancellationToken);
    }

    public override async Task<IReadOnlyList<TMainEntity>> GetAllActiveAsync(int top, int skip, CancellationToken cancellationToken = default)
    {
        return await ExecuteCommand(() => GetAllAsyncActiveCommand(top, skip, cancellationToken));
    }
    private async Task<IReadOnlyList<TMainEntity>> GetAllAsyncActiveCommand(int top, int skip, CancellationToken cancellationToken = default)
    {
        return await Context.Set<TMainEntity>().Where(x => !x.Deleted && x.Active).OrderBy(x => x.Order).ThenByDescending(x => x.Id).Take(top).Skip(skip).ToListAsync(cancellationToken);
    }

    public override async Task<IReadOnlyList<TMainEntity>> GetAllActiveAsync(Expression<Func<TMainEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await ExecuteCommand(() => GetAllActiveAsyncCommand(expression, cancellationToken));
    }
    private async Task<IReadOnlyList<TMainEntity>> GetAllActiveAsyncCommand(Expression<Func<TMainEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await Context.Set<TMainEntity>().Where(expression).Where(x => !x.Deleted && x.Active).OrderBy(x => x.Order).ThenByDescending(x => x.Id).ToListAsync(cancellationToken);
    }

    public override async Task<IReadOnlyList<TMainEntity>> GetAllActiveAsync(Expression<Func<TMainEntity, bool>> expression, int top, int skip, CancellationToken cancellationToken = default)
    {
        return await ExecuteCommand(() => GetAllActiveAsyncCommand(expression, top, skip, cancellationToken));
    }
    private async Task<IReadOnlyList<TMainEntity>> GetAllActiveAsyncCommand(Expression<Func<TMainEntity, bool>> expression, int top, int skip, CancellationToken cancellationToken = default)
    {
        return await Context.Set<TMainEntity>().Where(expression).Where(x => !x.Deleted).OrderBy(x => x.Order).ThenByDescending(x => x.Id).Take(top).Skip(skip).ToListAsync(cancellationToken);
    }

    #endregion
}