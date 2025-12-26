using Microsoft.Extensions.DependencyInjection;
using Hamkare.Service.ServiceProviders;

namespace Hamkare.Service;

public static class BaseServiceProvider
{
    public static void BaseService(this IServiceCollection serviceCollection)
    {
        serviceCollection.General();
        serviceCollection.Identity();
        serviceCollection.News();
        serviceCollection.Order();
        serviceCollection.Person();
        serviceCollection.Order();
    }
}