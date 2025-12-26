using Hamkare.Common.Interface.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hamkare.Common.Interface.Repositories;

public interface IMainRepository<TMainEntity, TDbContext> : IRootRepository<TMainEntity, TDbContext> where TMainEntity : IMainEntity, new() where TDbContext : DbContext
{
}