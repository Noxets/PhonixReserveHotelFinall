using Hamkare.Common.Entities.News;
using Hamkare.Common.Interface.Repositories;
using Hamkare.Infrastructure;
using Hamkare.Service.Services.Generic;

namespace Hamkare.Service.Services.News;

public class NewsCommentService(ICommentRepository<NewsComment, ApplicationDbContext> repository) : CommentService<NewsComment, ApplicationDbContext>(repository);