using Hamkare.Common.Interface.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hamkare.Common.Interface.Repositories;

public interface ICommentRepository<TCommentEntity, TDbContext> : IRootRepository<TCommentEntity, TDbContext> where TCommentEntity : ICommentEntity, new() where TDbContext : DbContext
{
}