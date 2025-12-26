using Hamkare.Service.Services.News;
using Microsoft.Extensions.DependencyInjection;

namespace Hamkare.Service.ServiceProviders;

public static class NewsServiceProvider
{
    internal static void News(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<NewsService>();
        serviceCollection.AddTransient<NewsCommentService>();
        serviceCollection.AddTransient<NewsCategoryService>();
    }
}