using Hamkare.Common.Interface.Entities;
using Hamkare.Common.Interface.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Hamkare.Infrastructure.Repository;

public class CommentRepository<TCommentEntity, TDbContext>(IDbContextFactory<TDbContext> context) : RootRepository<TCommentEntity, TDbContext>(context), ICommentRepository<TCommentEntity, TDbContext>
    where TCommentEntity : class, ICommentEntity, new() where TDbContext : DbContext;