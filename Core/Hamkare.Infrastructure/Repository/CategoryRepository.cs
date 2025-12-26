using Hamkare.Common.Interface.Entities;
using Hamkare.Common.Interface.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Hamkare.Infrastructure.Repository;

public class CategoryRepository<TGroupEntity, TDbContext>(IDbContextFactory<TDbContext> context) : MainRepository<TGroupEntity, TDbContext>(context), ICategoryRepository<TGroupEntity, TDbContext>
    where TGroupEntity : class, ICategoryEntity<TGroupEntity>, new() where TDbContext : DbContext;