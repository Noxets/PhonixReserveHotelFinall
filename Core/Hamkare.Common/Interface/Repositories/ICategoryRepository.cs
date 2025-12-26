using Hamkare.Common.Interface.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hamkare.Common.Interface.Repositories;

public interface ICategoryRepository<TGroupEntity, TDbContext> : IMainRepository<TGroupEntity, TDbContext> where TGroupEntity : ICategoryEntity<TGroupEntity>, new() where TDbContext : DbContext
{
}