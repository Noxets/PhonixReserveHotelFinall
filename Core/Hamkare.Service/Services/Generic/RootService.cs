using System.Linq.Expressions;
using Hamkare.Common.Interface.Entities;
using Hamkare.Common.Interface.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Hamkare.Service.Services.Generic;

public class RootService<TEntity, TDbContext> where TEntity : IRootEntity, new() where TDbContext : DbContext
{
    private readonly IRootRepository<TEntity, TDbContext> _repository;

    protected RootService(IRootRepository<TEntity, TDbContext> repository)
    {
        _repository = repository;
    }

    public virtual async Task AddOrUpdateAsync(TEntity newItem, CancellationToken cancellationToken = default)
    {
        await _repository.AddOrUpdate(newItem, cancellationToken);
    }

    public virtual async Task ActiveOrDeactivateAsync(long id, CancellationToken cancellationToken = default)
    {
        await _repository.ActiveOrDeactivate(id, cancellationToken);
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _repository.Delete(entity, cancellationToken);
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var item = await FindAsync(id, cancellationToken);
        await _repository.Delete(item, cancellationToken);
    }

    public void Detached(IRootEntity entity)
    {
        _repository.Detached(entity);
    }

    public void Detached(IEnumerable<IRootEntity> entities)
    {
        _repository.Detached(entities);
    }

    public void Modified(IRootEntity entity)
    {
        _repository.Modified(entity);
    }

    public void Modified(IEnumerable<IRootEntity> entities)
    {
        _repository.Modified(entities);
    }

    #region Get

    public Task<TEntity> FindAsync(long id, CancellationToken cancellationToken = default)
    {
        return _repository.FindAsync(id, cancellationToken);
    }

    public Task<TEntity> GetActiveAsync(CancellationToken cancellationToken = default)
    {
        return _repository.GetActiveAsync(cancellationToken);
    }

    public Task<TEntity> GetActiveAsync(long id, CancellationToken cancellationToken = default)
    {
        return _repository.GetActiveAsync(id, cancellationToken);
    }

    public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken = default)
    {
        return _repository.GetAsync(expression, cancellationToken);
    }

    public Task<TEntity> GetActiveAsync(Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken = default)
    {
        return _repository.GetActiveAsync(expression, cancellationToken);
    }

    #endregion

    #region Get All

    public Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _repository.GetAllAsync(cancellationToken);
    }

    public Task<IReadOnlyList<TEntity>> GetAllActiveAsync(CancellationToken cancellationToken = default)
    {
        return _repository.GetAllActiveAsync(cancellationToken);
    }

    public Task<IReadOnlyList<TEntity>> GetAllAsync(int top, int skip, CancellationToken cancellationToken = default)
    {
        return _repository.GetAllAsync(top, skip, cancellationToken);
    }

    public Task<IReadOnlyList<TEntity>> GetAllActiveAsync(int top, int skip,
        CancellationToken cancellationToken = default)
    {
        return _repository.GetAllActiveAsync(top, skip, cancellationToken);
    }

    public Task<IReadOnlyList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken = default)
    {
        return _repository.GetAllAsync(expression, cancellationToken);
    }

    public Task<IReadOnlyList<TEntity>> GetAllActiveAsync(Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken = default)
    {
        return _repository.GetAllActiveAsync(expression, cancellationToken);
    }

    public Task<IReadOnlyList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, int top, int skip,
        CancellationToken cancellationToken = default)
    {
        return _repository.GetAllAsync(expression, top, skip, cancellationToken);
    }

    public Task<IReadOnlyList<TEntity>> GetAllActiveAsync(Expression<Func<TEntity, bool>> expression, int top, int skip,
        CancellationToken cancellationToken = default)
    {
        return _repository.GetAllActiveAsync(expression, top, skip, cancellationToken);
    }

    #endregion

    #region Count

    public Task<long> GetCountAsync(CancellationToken cancellationToken = default)
    {
        return _repository.CountAsync(cancellationToken);
    }

    public Task<long> GetCountAsync(Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken = default)
    {
        return _repository.CountAsync(expression, cancellationToken);
    }

    public Task<long> GetCountActiveAsync(CancellationToken cancellationToken = default)
    {
        return _repository.CountActiveAsync(cancellationToken);
    }

    public Task<long> GetCountActiveAsync(Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken = default)
    {
        return _repository.CountActiveAsync(expression, cancellationToken);
    }

    #endregion

    #region Any

    public Task<bool> GetAnyAsync(CancellationToken cancellationToken = default)
    {
        return _repository.AnyAsync(cancellationToken);
    }

    public Task<bool> GetAnyAsync(Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken = default)
    {
        return _repository.AnyAsync(expression, cancellationToken);
    }

    public Task<bool> GetAnyActiveAsync(CancellationToken cancellationToken = default)
    {
        return _repository.AnyActiveAsync(cancellationToken);
    }

    public Task<bool> GetAnyActiveAsync(Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken = default)
    {
        return _repository.AnyActiveAsync(expression, cancellationToken);
    }

    #endregion
}