using Hamkare.Common.Interface.Repositories;
using Hamkare.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Hamkare.Infrastructure;

public static class BaseServiceProvider
{
    public static void BaseRepository(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient(typeof(IRootRepository<,>), typeof(RootRepository<,>));
        serviceCollection.AddTransient(typeof(IMainRepository<,>), typeof(MainRepository<,>));
        serviceCollection.AddTransient(typeof(ICommentRepository<,>), typeof(CommentRepository<,>));
        serviceCollection.AddTransient(typeof(ICategoryRepository<,>), typeof(CategoryRepository<,>));
    }
}