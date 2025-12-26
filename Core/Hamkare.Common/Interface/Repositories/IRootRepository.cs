using System.Linq.Expressions;
using Hamkare.Common.Interface.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hamkare.Common.Interface.Repositories;

public interface IRootRepository<TEntity, TDbContext> where TEntity : IRootEntity, new() where TDbContext : DbContext
{
    Task<TEntity> AddOrUpdate(TEntity entity, CancellationToken cancellationToken = default);

    Task<TEntity> ActiveOrDeactivate(long id, CancellationToken cancellationToken = default);

    Task<long> Delete(TEntity entity, CancellationToken cancellationToken = default);


    void Detached(IRootEntity entity);

    void Detached(IEnumerable<IRootEntity> entities);

    void Modified(IRootEntity entity);

    void Modified(IEnumerable<IRootEntity> entities);

    #region Get

    Task<TEntity> FindAsync(long id, CancellationToken cancellationToken = default);
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
    Task<TEntity> GetActiveAsync(CancellationToken cancellationToken = default);
    Task<TEntity> GetActiveAsync(long id, CancellationToken cancellationToken = default);

    Task<TEntity> GetActiveAsync(Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken = default);

    #endregion

    #region Get All

    Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TEntity>> GetAllAsync(int top, int skip, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, int top, int skip,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TEntity>> GetAllActiveAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TEntity>> GetAllActiveAsync(int top, int skip, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TEntity>> GetAllActiveAsync(Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TEntity>> GetAllActiveAsync(Expression<Func<TEntity, bool>> expression, int top, int skip,
        CancellationToken cancellationToken = default);

    #endregion

    #region Count

    Task<long> CountAsync(CancellationToken cancellationToken = default);
    Task<long> CountAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);

    Task<long> CountActiveAsync(CancellationToken cancellationToken = default);

    Task<long> CountActiveAsync(Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken = default);

    #endregion

    #region Any

    Task<bool> AnyAsync(CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);

    Task<bool> AnyActiveAsync(CancellationToken cancellationToken = default);

    Task<bool> AnyActiveAsync(Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken = default);

    #endregion
}