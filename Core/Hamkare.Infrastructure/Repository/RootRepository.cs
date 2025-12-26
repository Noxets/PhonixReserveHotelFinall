using System.Linq.Expressions;
using Hamkare.Common.Interface.Entities;
using Hamkare.Common.Interface.Repositories;
using Hamkare.Resource;
using Microsoft.EntityFrameworkCore;

namespace Hamkare.Infrastructure.Repository;

public class RootRepository<TRootEntity, TDbContext>(IDbContextFactory<TDbContext> context)
    : IRootRepository<TRootEntity, TDbContext>
    where TRootEntity : class, IRootEntity, new() where TDbContext : DbContext
{
    #region Constructor

    protected readonly TDbContext Context = context.CreateDbContext();

    #endregion

    #region Get

    public virtual async Task<TRootEntity> FindAsync(long id, CancellationToken cancellationToken = default)
    {
        return await ExecuteCommand(() => GetAsyncCommand(id, cancellationToken));
    }

    private async Task<TRootEntity> GetAsyncCommand(long id, CancellationToken cancellationToken = default)
    {
        return await Context.Set<TRootEntity>().FindAsync([id], cancellationToken: cancellationToken);
    }

    public virtual async Task<TRootEntity> GetAsync(Expression<Func<TRootEntity, bool>> expression,
        CancellationToken cancellationToken = default)
    {
        return await ExecuteCommand(() => GetAsyncCommand(expression, cancellationToken));
    }

    private async Task<TRootEntity> GetAsyncCommand(Expression<Func<TRootEntity, bool>> expression,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<TRootEntity>().Where(x => !x.Deleted)
            .FirstOrDefaultAsync(expression, cancellationToken);
    }

    #endregion

    #region Get Active

    public virtual async Task<TRootEntity> GetActiveAsync(CancellationToken cancellationToken = default)
    {
        return await ExecuteCommand(() => GetActiveAsyncCommand(cancellationToken));
    }

    private async Task<TRootEntity> GetActiveAsyncCommand(CancellationToken cancellationToken = default)
    {
        return await Context.Set<TRootEntity>().SingleOrDefaultAsync(x => x.Active && !x.Deleted,
            cancellationToken: cancellationToken);
    }

    public virtual async Task<TRootEntity> GetActiveAsync(long id, CancellationToken cancellationToken = default)
    {
        return await ExecuteCommand(() => GetActiveAsyncCommand(id, cancellationToken));
    }

    private async Task<TRootEntity> GetActiveAsyncCommand(long id, CancellationToken cancellationToken = default)
    {
        return await Context.Set<TRootEntity>().SingleOrDefaultAsync(x => x.Id == id && x.Active && !x.Deleted,
            cancellationToken: cancellationToken);
    }

    public virtual async Task<TRootEntity> GetActiveAsync(Expression<Func<TRootEntity, bool>> expression,
        CancellationToken cancellationToken = default)
    {
        return await ExecuteCommand(() => GetActiveAsyncCommand(expression, cancellationToken));
    }

    private async Task<TRootEntity> GetActiveAsyncCommand(Expression<Func<TRootEntity, bool>> expression,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<TRootEntity>().Where(x => !x.Deleted && x.Active)
            .FirstOrDefaultAsync(expression, cancellationToken);
    }

    #endregion

    #region Get All

    public virtual async Task<IReadOnlyList<TRootEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await ExecuteCommand(() => GetAllAsyncCommand(cancellationToken));
    }

    private async Task<IReadOnlyList<TRootEntity>> GetAllAsyncCommand(CancellationToken cancellationToken = default)
    {
        return await Context.Set<TRootEntity>().Where(x => !x.Deleted).OrderByDescending(x => x.Id)
            .ToListAsync(cancellationToken);
    }

    public virtual async Task<IReadOnlyList<TRootEntity>> GetAllAsync(int top, int skip,
        CancellationToken cancellationToken = default)
    {
        return await ExecuteCommand(() => GetAllAsyncCommand(top, skip, cancellationToken));
    }

    private async Task<IReadOnlyList<TRootEntity>> GetAllAsyncCommand(int top, int skip,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<TRootEntity>().Where(x => !x.Deleted).OrderByDescending(x => x.Id).Take(top).Skip(skip)
            .ToListAsync(cancellationToken);
    }

    public virtual async Task<IReadOnlyList<TRootEntity>> GetAllAsync(Expression<Func<TRootEntity, bool>> expression,
        CancellationToken cancellationToken = default)
    {
        return await ExecuteCommand(() => GetAllAsyncCommand(expression, cancellationToken));
    }

    private async Task<IReadOnlyList<TRootEntity>> GetAllAsyncCommand(Expression<Func<TRootEntity, bool>> expression,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<TRootEntity>().Where(expression).Where(x => !x.Deleted).OrderByDescending(x => x.Id)
            .ToListAsync(cancellationToken);
    }

    public virtual async Task<IReadOnlyList<TRootEntity>> GetAllAsync(Expression<Func<TRootEntity, bool>> expression,
        int top, int skip, CancellationToken cancellationToken = default)
    {
        return await ExecuteCommand(() => GetAllAsyncCommand(expression, top, skip, cancellationToken));
    }

    private async Task<IReadOnlyList<TRootEntity>> GetAllAsyncCommand(Expression<Func<TRootEntity, bool>> expression,
        int top, int skip, CancellationToken cancellationToken = default)
    {
        return await Context.Set<TRootEntity>().Where(expression).Where(x => !x.Deleted).OrderByDescending(x => x.Id)
            .Take(top).Skip(skip).ToListAsync(cancellationToken);
    }

    #endregion

    #region Get All Active

    public virtual async Task<IReadOnlyList<TRootEntity>> GetAllActiveAsync(
        CancellationToken cancellationToken = default)
    {
        return await ExecuteCommand(() => GetAllActiveAsyncCommand(cancellationToken));
    }

    private async Task<IReadOnlyList<TRootEntity>> GetAllActiveAsyncCommand(
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<TRootEntity>().Where(x => !x.Deleted && x.Active).OrderByDescending(x => x.Id)
            .ToListAsync(cancellationToken);
    }

    public virtual async Task<IReadOnlyList<TRootEntity>> GetAllActiveAsync(int top, int skip,
        CancellationToken cancellationToken = default)
    {
        return await ExecuteCommand(() => GetAllAsyncActiveCommand(top, skip, cancellationToken));
    }

    private async Task<IReadOnlyList<TRootEntity>> GetAllAsyncActiveCommand(int top, int skip,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<TRootEntity>().Where(x => !x.Deleted && x.Active).OrderByDescending(x => x.Id)
            .Take(top).Skip(skip).ToListAsync(cancellationToken);
    }

    public virtual async Task<IReadOnlyList<TRootEntity>> GetAllActiveAsync(
        Expression<Func<TRootEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await ExecuteCommand(() => GetAllActiveAsyncCommand(expression, cancellationToken));
    }

    private async Task<IReadOnlyList<TRootEntity>> GetAllActiveAsyncCommand(
        Expression<Func<TRootEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await Context.Set<TRootEntity>().Where(expression).Where(x => !x.Deleted && x.Active)
            .OrderByDescending(x => x.Id).ToListAsync(cancellationToken);
    }

    public virtual async Task<IReadOnlyList<TRootEntity>> GetAllActiveAsync(
        Expression<Func<TRootEntity, bool>> expression, int top, int skip,
        CancellationToken cancellationToken = default)
    {
        return await ExecuteCommand(() => GetAllActiveAsyncCommand(expression, top, skip, cancellationToken));
    }

    private async Task<IReadOnlyList<TRootEntity>> GetAllActiveAsyncCommand(
        Expression<Func<TRootEntity, bool>> expression, int top, int skip,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<TRootEntity>().Where(expression).Where(x => !x.Deleted).OrderByDescending(x => x.Id)
            .Take(top).Skip(skip).ToListAsync(cancellationToken);
    }

    #endregion

    #region Count

    public virtual async Task<long> CountAsync(CancellationToken cancellationToken = default)
    {
        return await ExecuteCommand(() => CountAsyncCommand(cancellationToken));
    }

    private async Task<long> CountAsyncCommand(CancellationToken cancellationToken = default)
    {
        return await Context.Set<TRootEntity>().CountAsync(x => !x.Deleted, cancellationToken);
    }

    public virtual async Task<long> CountAsync(Expression<Func<TRootEntity, bool>> expression,
        CancellationToken cancellationToken = default)
    {
        return await ExecuteCommand(() => CountAsyncCommand(expression, cancellationToken));
    }

    private async Task<long> CountAsyncCommand(Expression<Func<TRootEntity, bool>> expression,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<TRootEntity>().Where(x => !x.Deleted).CountAsync(expression, cancellationToken);
    }

    #endregion

    #region Count Active

    public virtual async Task<long> CountActiveAsync(CancellationToken cancellationToken = default)
    {
        return await ExecuteCommand(() => CountActiveAsyncCommand(cancellationToken));
    }

    private async Task<long> CountActiveAsyncCommand(CancellationToken cancellationToken = default)
    {
        return await Context.Set<TRootEntity>().CountAsync(x => !x.Deleted && x.Active, cancellationToken);
    }

    public virtual async Task<long> CountActiveAsync(Expression<Func<TRootEntity, bool>> expression,
        CancellationToken cancellationToken = default)
    {
        return await ExecuteCommand(() => CountActiveAsyncCommand(expression, cancellationToken));
    }

    private async Task<long> CountActiveAsyncCommand(Expression<Func<TRootEntity, bool>> expression,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<TRootEntity>().Where(x => !x.Deleted && x.Active)
            .CountAsync(expression, cancellationToken);
    }

    #endregion

    #region Any

    public virtual async Task<bool> AnyAsync(CancellationToken cancellationToken = default)
    {
        return await ExecuteCommand(() => AnyAsyncCommand(cancellationToken));
    }

    private async Task<bool> AnyAsyncCommand(CancellationToken cancellationToken = default)
    {
        return await Context.Set<TRootEntity>().AnyAsync(x => !x.Deleted, cancellationToken);
    }

    public virtual async Task<bool> AnyAsync(Expression<Func<TRootEntity, bool>> expression,
        CancellationToken cancellationToken = default)
    {
        return await ExecuteCommand(() => AnyAsyncCommand(expression, cancellationToken));
    }

    private async Task<bool> AnyAsyncCommand(Expression<Func<TRootEntity, bool>> expression,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<TRootEntity>().Where(x => !x.Deleted).AnyAsync(expression, cancellationToken);
    }

    #endregion

    #region Any Active

    public virtual async Task<bool> AnyActiveAsync(CancellationToken cancellationToken = default)
    {
        return await ExecuteCommand(() => AnyActiveAsyncCommand(cancellationToken));
    }

    private async Task<bool> AnyActiveAsyncCommand(CancellationToken cancellationToken = default)
    {
        return await Context.Set<TRootEntity>().AnyAsync(x => !x.Deleted && x.Active, cancellationToken);
    }

    public virtual async Task<bool> AnyActiveAsync(Expression<Func<TRootEntity, bool>> expression,
        CancellationToken cancellationToken = default)
    {
        return await ExecuteCommand(() => AnyActiveAsyncCommand(expression, cancellationToken));
    }

    private async Task<bool> AnyActiveAsyncCommand(Expression<Func<TRootEntity, bool>> expression,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<TRootEntity>().Where(x => !x.Deleted && x.Active)
            .AnyAsync(expression, cancellationToken);
    }

    #endregion

    #region AddOrUpdate

    public virtual async Task<TRootEntity> AddOrUpdate(TRootEntity entity,
        CancellationToken cancellationToken = default)
    {
        return await ExecuteCommand(() => AddOrUpdateCommand(entity, cancellationToken));
    }

    private async Task<TRootEntity> AddOrUpdateCommand(TRootEntity entity,
        CancellationToken cancellationToken = default)
    {
        if (entity.Id == 0)
            await Context.Set<TRootEntity>().AddAsync(entity, cancellationToken);
        else
            Context.Entry(entity).State = EntityState.Modified;

        await Context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public virtual async Task<TRootEntity> ActiveOrDeactivate(long id, CancellationToken cancellationToken = default)
    {
        var item = await FindAsync(id, cancellationToken);
        return await ExecuteCommand(() => ActiveOrDeactivateCommand(item, cancellationToken));
    }

    private async Task<TRootEntity> ActiveOrDeactivateCommand(TRootEntity entity,
        CancellationToken cancellationToken = default)
    {
        entity.Active = !entity.Active;
        Context.Entry(entity).State = EntityState.Modified;

        await Context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    #endregion

    #region Delete

    public virtual async Task<long> Delete(TRootEntity entity, CancellationToken cancellationToken = default)
    {
        if (entity.System)
            throw new Exception(@ErrorResources.SystematicData)
            {
                HelpLink = null,
                HResult = 0,
                Source = "Validate"
            };

        return await ExecuteCommand(() => DeleteCommand(entity, cancellationToken));
    }

    private async Task<long> DeleteCommand(TRootEntity entity, CancellationToken cancellationToken = default)
    {
        if (HasRelationships())
        {
            entity.Deleted = true;
            Context.Entry(entity).State = EntityState.Modified;
            return await Context.SaveChangesAsync(cancellationToken);
        }

        Context.Set<TRootEntity>().Remove(entity);
        return await Context.SaveChangesAsync(cancellationToken);
    }

    #endregion

    #region Other

    public virtual void Detached(IRootEntity entity)
    {
        Context.Entry(entity).State = EntityState.Detached;
    }

    public virtual void Detached(IEnumerable<IRootEntity> entities)
    {
        foreach (var item in entities)
            Context.Entry(item).State = EntityState.Detached;
    }

    public virtual void Modified(IRootEntity entity)
    {
        Context.Entry(entity).State = EntityState.Modified;
    }

    public virtual void Modified(IEnumerable<IRootEntity> entities)
    {
        foreach (var item in entities)
            Context.Entry(item).State = EntityState.Modified;
    }

    protected async Task<T> ExecuteCommand<T>(Func<Task<T>> command)
    {
        if (Context.Database.CurrentTransaction != null)
            return await command.Invoke();

        await using var transaction = await Context.Database.BeginTransactionAsync();
        try
        {
            var result = await command.Invoke();
            await transaction.CommitAsync();
            return result;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    private bool HasRelationships()
    {
        var entityType = Context.Model.FindEntityType(typeof(TRootEntity));

        return entityType?.GetNavigations().Any() ?? false;
    }

    #endregion
}